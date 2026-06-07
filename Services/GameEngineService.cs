using NaoComemoreAntesdoFim.Models;

namespace NaoComemoreAntesdoFim.Services;

public partial class GameEngineService
{
    public const int AnswerRevealTimeoutSeconds = GameRules.AnswerTimeLimitSeconds;
    public const int MaxGameDurationHours = GameRules.MaxGameDurationHours;
    public const int MaxPlayers = 10;
    public const int QuestionDrawChancePercent = 70;

    private readonly Random _random = new();
    private List<Card> _questionDeck = [];
    private List<Card> _actionDeck = [];
    private DateTime? _gameStartedAtUtc;
    private DateTime? _gameEndsAtUtc;
    private TimeSpan? _sessionDurationLimit;

    public IReadOnlyList<Player> Players => _players;
    public Card? CurrentCard { get; private set; }
    public int CurrentPlayerIndex { get; private set; }
    public bool IsGameStarted { get; private set; }
    public bool IsGameEnded { get; private set; }
    public bool ShowAnswer { get; private set; }
    public bool CardRevealed { get; private set; }
    public bool EffectApplied { get; private set; }
    public bool? LastQuestionWasCorrect { get; private set; }
    public GameEndReason EndReason { get; private set; }
    public Guid? WinnerPlayerId { get; private set; }
    public bool SocialChallengePending { get; private set; }
    /// <summary>Opção selecionada pelo jogador ('A', 'B' ou 'C'). Nulo se ainda não selecionou.</summary>
    public char? SelectedOption { get; private set; }

    private readonly List<Player> _players = [];
    private bool _turnAdvancedByCard;

    public event Action? StateChanged;

    public int QuestionsRemaining => _questionDeck.Count;
    public int ActionCardsRemaining => _actionDeck.Count;
    public int CardsRemaining => QuestionsRemaining + ActionCardsRemaining;

    public TimeSpan? SessionDurationLimit => _sessionDurationLimit;

    public DateTime? GameStartedAtUtc => _gameStartedAtUtc;

    public DateTime? GameEndsAtUtc => _gameEndsAtUtc;

    public TimeSpan Elapsed =>
        _gameStartedAtUtc.HasValue
            ? DateTime.UtcNow - _gameStartedAtUtc.Value
            : TimeSpan.Zero;

    public TimeSpan? TimeRemaining
    {
        get
        {
            if (!_gameEndsAtUtc.HasValue)
                return null;

            var remaining = _gameEndsAtUtc.Value - DateTime.UtcNow;
            return remaining < TimeSpan.Zero ? TimeSpan.Zero : remaining;
        }
    }

    public Player? CurrentPlayer =>
        _players.Count > 0 ? _players[CurrentPlayerIndex] : null;

    public void InitializeGame(IEnumerable<string> playerNames, TimeSpan? sessionDurationLimit = null)
    {
        _players.Clear();
        foreach (var name in playerNames.Where(n => !string.IsNullOrWhiteSpace(n)))
            _players.Add(new Player { Name = name.Trim() });

        CurrentPlayerIndex = 0;
        CurrentCard = null;
        ShowAnswer = false;
        CardRevealed = false;
        EffectApplied = false;
        LastQuestionWasCorrect = null;
        SelectedOption = null;
        _turnAdvancedByCard = false;
        SocialChallengePending = false;
        EndReason = GameEndReason.None;
        WinnerPlayerId = null;
        IsGameEnded = false;
        _sessionDurationLimit = sessionDurationLimit;
        _gameStartedAtUtc = null;
        _gameEndsAtUtc = null;
        IsGameStarted = _players.Count >= 2;

        if (IsGameStarted)
        {
            SplitDecks(LoadCards());
            StartGameClock();
        }

        NotifyStateChanged();
    }

    public Card DrawCard()
    {
        if (!IsGameStarted || IsGameEnded)
            throw new InvalidOperationException("O jogo não está pronto para sortear.");

        CheckTimeLimit();

        if (IsGameEnded)
            throw new InvalidOperationException("O jogo já terminou.");

        if (_questionDeck.Count == 0)
        {
            TryFinishBecauseQuestionsExhausted();
            throw new InvalidOperationException("As cartas de pergunta acabaram.");
        }

        var card = DrawNextCardFromDecks();

        CurrentCard = card;
        ShowAnswer = false;
        CardRevealed = false;
        EffectApplied = false;
        LastQuestionWasCorrect = null;
        SelectedOption = null;
        _turnAdvancedByCard = false;
        SocialChallengePending = false;

        if (!IsQuestionCard(card) && !RequiresTargetPlayer(card.ActionCode) && !RequiresDonationTargets(card.ActionCode))
            ApplyAction(card.ActionCode, null);

        NotifyStateChanged();
        return card;
    }

    public void RevealCard()
    {
        CardRevealed = true;
        NotifyStateChanged();
    }

    public void RevealAnswer()
    {
        ShowAnswer = true;
        NotifyStateChanged();
    }

    public void RecordQuestionResult(bool correct)
    {
        if (CurrentCard is null || !IsQuestionCard(CurrentCard) || EffectApplied)
            return;

        var current = CurrentPlayer;
        if (current is null)
            return;

        LastQuestionWasCorrect = correct;
        if (correct)
        {
            current.Chocolates += GameRules.ChocolatesPerCorrectAnswer;
            current.Points += GameRules.PointsPerCorrectAnswer;
        }

        EffectApplied = true;
        CheckTimeLimit();
        NotifyStateChanged();
    }

    /// <summary>
    /// Chamado quando o jogador seleciona uma das opções A/B/C.
    /// Valida automaticamente e registra o resultado sem árbitro humano.
    /// </summary>
    public void SelectMultipleChoiceOption(char selectedOption)
    {
        if (CurrentCard is null || !IsQuestionCard(CurrentCard) || EffectApplied)
            return;

        if (!CurrentCard.HasMultipleChoice)
            return;

        var isCorrect = char.ToUpper(selectedOption) == char.ToUpper(CurrentCard.CorrectOption);
        SelectedOption = char.ToUpper(selectedOption);

        // Garante que a resposta seja revelada junto com o resultado
        ShowAnswer = true;
        RecordQuestionResult(isCorrect);
    }

    public void ConfirmSocialChallenge()
    {
        if (!SocialChallengePending)
            return;

        SocialChallengePending = false;
        EffectApplied = true;
        NotifyStateChanged();
    }

    public bool CurrentQuestionNeedsGrading()
    {
        if (CurrentCard is null || !IsQuestionCard(CurrentCard) || EffectApplied)
            return false;

        // Cartas com múltipla escolha se auto-corrigem — não precisam de árbitro
        if (CurrentCard.HasMultipleChoice)
            return false;

        return ShowAnswer;
    }

    public bool CanPassTurn()
    {
        if (!IsGameStarted || IsGameEnded || CurrentCard is null)
            return false;

        // Carta de pergunta: precisa ter sido corrigida (efeito aplicado)
        if (IsQuestionCard(CurrentCard) && !EffectApplied)
            return false;

        // Carta que precisa de seleção de alvo ainda não foi aplicada
        if (CurrentCardNeedsTargetSelection())
            return false;

        // Carta de doação ainda não aplicada
        if (CurrentCardNeedsDonationTargets())
            return false;

        // Desafio social ainda pendente de confirmação
        if (SocialChallengePending)
            return false;

        return true;
    }

    public void EndGame()
    {
        if (!IsGameStarted)
            return;

        FinishGame(GameEndReason.Manual, null);
    }

    public void CheckTimeLimit()
    {
        if (!IsGameStarted || IsGameEnded || !_gameEndsAtUtc.HasValue)
            return;

        if (DateTime.UtcNow >= _gameEndsAtUtc.Value)
            FinishGame(GameEndReason.TimeExpired, null);
    }

    public void RefreshEndConditions()
    {
        CheckTimeLimit();
        TryFinishBecauseQuestionsExhausted();
    }

    public IReadOnlyList<Player> GetRankedPlayers() =>
        _players
            .OrderByDescending(p => p.Points)
            .ThenByDescending(p => p.Chocolates)
            .ThenBy(p => p.Name)
            .ToList();

    public void ResetGame()
    {
        _players.Clear();
        _questionDeck = [];
        _actionDeck = [];
        CurrentCard = null;
        CurrentPlayerIndex = 0;
        IsGameStarted = false;
        IsGameEnded = false;
        ShowAnswer = false;
        CardRevealed = false;
        EffectApplied = false;
        LastQuestionWasCorrect = null;
        SelectedOption = null;
        _turnAdvancedByCard = false;
        SocialChallengePending = false;
        EndReason = GameEndReason.None;
        WinnerPlayerId = null;
        _gameStartedAtUtc = null;
        _gameEndsAtUtc = null;
        _sessionDurationLimit = null;
        NotifyStateChanged();
    }

    public void ApplyActionToTarget(Guid targetPlayerId) =>
        ApplyActionToTargets(targetPlayerId, null);

    public void ApplyActionToTargets(Guid? primaryTargetId, Guid? secondaryTargetId)
    {
        if (CurrentCard is null || EffectApplied)
            return;

        ApplyCardEffect(CurrentCard, primaryTargetId, secondaryTargetId);
        NotifyStateChanged();
    }

    public void PassTurn()
    {
        if (!IsGameStarted || _players.Count == 0 || IsGameEnded)
            return;

        // Proteção: só passa se o estado atual permite
        if (!CanPassTurn())
            return;

        CheckTimeLimit();
        if (IsGameEnded)
            return;

        CurrentCard = null;
        ShowAnswer = false;
        CardRevealed = false;
        EffectApplied = false;
        LastQuestionWasCorrect = null;
        SelectedOption = null;
        SocialChallengePending = false;

        if (!_turnAdvancedByCard)
            AdvanceToNextPlayer();

        _turnAdvancedByCard = false;

        TryFinishBecauseQuestionsExhausted();
        CheckTimeLimit();
        NotifyStateChanged();
    }

    private void AdvanceToNextPlayer()
    {
        if (_players.Count == 0)
            return;

        // Limita a quantos jogadores existem para evitar loop infinito
        var maxSteps = _players.Count;
        for (var step = 0; step < maxSteps; step++)
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % _players.Count;
            var next = _players[CurrentPlayerIndex];

            if (next.SkipNextTurn)
            {
                // Marca que este jogador perdeu a vez e continua procurando o próximo
                next.SkipNextTurn = false;
            }
            else
            {
                // Encontrou jogador válido
                return;
            }
        }
        // Se todos tinham SkipNextTurn, CurrentPlayerIndex voltou ao início — ok.
    }

    public Player? GetChocolateLeader() =>
        _players.OrderByDescending(p => p.Points).ThenByDescending(p => p.Chocolates).FirstOrDefault();

    public Player? GetWinnerPlayer() =>
        WinnerPlayerId is null ? null : _players.FirstOrDefault(p => p.Id == WinnerPlayerId);

    public IEnumerable<Player> GetOtherPlayers(Guid currentPlayerId) =>
        _players.Where(p => p.Id != currentPlayerId);

    public static bool RequiresTargetPlayer(string actionCode)
    {
        var code = NormalizeActionCode(actionCode);
        // Nota: "DrinkWater" é um desafio social (IsSocialChallengeCard) e NÃO requer seleção de alvo.
        return code is "SkipOtherTurn"
            or "StealAllChocolates"
            or "StealChocolate"
            or "GiveChocolate"
            or "GiveAllChocolates";
    }

    public bool CurrentCardNeedsTargetSelection()
    {
        if (CurrentCard is null || EffectApplied || SocialChallengePending)
            return false;

        if (RequiresDonationTargets(CurrentCard.ActionCode))
            return false;

        if (RequiresTargetPlayer(CurrentCard.ActionCode))
            return true;

        var code = NormalizeActionCode(CurrentCard.ActionCode);
        return code == "LoseAllChocolates" && CurrentPlayer?.Chocolates == 0;
    }

    public bool CurrentCardNeedsDonationTargets()
    {
        if (CurrentCard is null || EffectApplied || SocialChallengePending)
            return false;

        return RequiresDonationTargets(CurrentCard.ActionCode);
    }

    public static bool RequiresDonationTargets(string actionCode) =>
        NormalizeActionCode(actionCode) == "DonateAllChocolates";

    public static bool IsSocialChallengeCard(string actionCode)
    {
        var code = NormalizeActionCode(actionCode);
        return code is "DrinkWater" or "DrinkWaterSelf" or "ReciteScripture" or "SingHymn" or "EatChocolate";
    }

    public static bool IsQuestionCard(Card? card) =>
        card?.Type == CardType.Question;

    private void ApplyAction(string actionCode, Guid? targetPlayerId) =>
        ApplyCardEffect(CurrentCard!, targetPlayerId, null);

    private void ApplyCardEffect(Card card, Guid? targetPlayerId, Guid? secondaryTargetId = null)
    {
        var current = CurrentPlayer;
        if (current is null)
            return;

        var code = NormalizeActionCode(card.ActionCode);
        var amount = ResolveAmount(card);

        switch (code)
        {
            case "GiveChocolates":
            case "GainChocolate":
                current.Chocolates += amount;
                EffectApplied = true;
                break;

            case "LoseAllChocolates":
                if (TryGetTarget(targetPlayerId, out var loseTarget))
                {
                    loseTarget.Chocolates = 0;
                    EffectApplied = true;
                }
                else if (current.Chocolates > 0)
                {
                    current.Chocolates = 0;
                    EffectApplied = true;
                }
                break;

            case "DrinkWater":
            case "DrinkWaterSelf":
            case "ReciteScripture":
            case "SingHymn":
            case "EatChocolate":
                SocialChallengePending = true;
                break;

            case "GiveAllChocolates":
                if (TryGetTarget(targetPlayerId, out var receiveTarget))
                {
                    receiveTarget.Chocolates += current.Chocolates;
                    current.Chocolates = 0;
                    EffectApplied = true;
                }
                break;

            case "DonateAllChocolates":
                if (TryGetTarget(targetPlayerId, out var donor)
                    && TryGetTarget(secondaryTargetId, out var recipient)
                    && donor.Id != recipient.Id
                    && donor.Id != current.Id
                    && recipient.Id != current.Id)
                {
                    recipient.Chocolates += donor.Chocolates;
                    donor.Chocolates = 0;
                    EffectApplied = true;
                }
                break;

            case "SkipTurn":
                EffectApplied = true;
                AdvanceToNextPlayer();
                _turnAdvancedByCard = true;
                break;

            case "SkipOtherTurn":
                if (TryGetTarget(targetPlayerId, out var skipTarget))
                {
                    skipTarget.SkipNextTurn = true;
                    EffectApplied = true;
                }
                break;

            case "StealAllChocolates":
                if (TryGetTarget(targetPlayerId, out var stealAllTarget))
                {
                    current.Chocolates += stealAllTarget.Chocolates;
                    stealAllTarget.Chocolates = 0;
                    EffectApplied = true;
                }
                break;

            case "StealChocolate":
                if (TryGetTarget(targetPlayerId, out var stealTarget))
                {
                    var stolen = Math.Min(amount, stealTarget.Chocolates);
                    stealTarget.Chocolates -= stolen;
                    current.Chocolates += stolen;
                    EffectApplied = true;
                }
                break;

            case "GiveChocolate":
                if (TryGetTarget(targetPlayerId, out var giveTarget))
                {
                    var toGive = Math.Min(amount, current.Chocolates);
                    current.Chocolates -= toGive;
                    giveTarget.Chocolates += toGive;
                    EffectApplied = true;
                }
                break;
        }
    }

    private bool TryGetTarget(Guid? targetPlayerId, out Player target)
    {
        target = null!;
        if (!targetPlayerId.HasValue)
            return false;

        var found = _players.FirstOrDefault(p => p.Id == targetPlayerId.Value);
        if (found is null)
            return false;

        target = found;
        return true;
    }

    private static int ResolveAmount(Card card)
    {
        if (card.Amount > 0)
            return card.Amount;

        return ParseAmountFromActionCode(card.ActionCode, 1);
    }

    private static int ParseAmountFromActionCode(string actionCode, int defaultAmount)
    {
        var parts = actionCode.Split(':', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 1 && int.TryParse(parts[1], out var amount) ? amount : defaultAmount;
    }

    private static string NormalizeActionCode(string actionCode)
    {
        if (string.IsNullOrWhiteSpace(actionCode))
            return string.Empty;

        return actionCode.Split(':', StringSplitOptions.RemoveEmptyEntries)[0];
    }

    private void SplitDecks(IEnumerable<Card> allCards)
    {
        var list = allCards.ToList();
        _questionDeck = list.Where(IsQuestionCard).OrderBy(_ => _random.Next()).ToList();
        _actionDeck = list.Where(c => !IsQuestionCard(c)).OrderBy(_ => _random.Next()).ToList();
    }

    private Card DrawNextCardFromDecks()
    {
        EnsureActionDeckHasCards();

        var preferQuestion = _questionDeck.Count > 0
            && (_actionDeck.Count == 0 || _random.Next(100) < QuestionDrawChancePercent);

        if (preferQuestion)
            return DrawFromDeck(_questionDeck);

        if (_actionDeck.Count > 0)
            return DrawFromDeck(_actionDeck);

        return DrawFromDeck(_questionDeck);
    }

    private void EnsureActionDeckHasCards()
    {
        if (_actionDeck.Count == 0)
            _actionDeck = LoadActionCards().OrderBy(_ => _random.Next()).ToList();
    }

    private Card DrawFromDeck(List<Card> deck)
    {
        var index = _random.Next(deck.Count);
        var card = deck[index];
        deck.RemoveAt(index);
        return card;
    }

    private void StartGameClock()
    {
        _gameStartedAtUtc = DateTime.UtcNow;
        var maxEnd = _gameStartedAtUtc.Value.AddHours(MaxGameDurationHours);
        if (_sessionDurationLimit.HasValue)
        {
            var configuredEnd = _gameStartedAtUtc.Value.Add(_sessionDurationLimit.Value);
            _gameEndsAtUtc = configuredEnd < maxEnd ? configuredEnd : maxEnd;
        }
        else
        {
            _gameEndsAtUtc = maxEnd;
        }
    }

    private void TryFinishBecauseQuestionsExhausted()
    {
        if (IsGameEnded || _questionDeck.Count > 0 || CurrentCard is not null)
            return;

        FinishGame(GameEndReason.QuestionsExhausted, null);
    }

    private void FinishGame(GameEndReason reason, Guid? winnerId)
    {
        IsGameEnded = true;
        EndReason = reason;
        WinnerPlayerId = winnerId ?? GetChocolateLeader()?.Id;
        CurrentCard = null;
        ShowAnswer = false;
        CardRevealed = false;
        EffectApplied = false;
        LastQuestionWasCorrect = null;
        SocialChallengePending = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => StateChanged?.Invoke();
}
