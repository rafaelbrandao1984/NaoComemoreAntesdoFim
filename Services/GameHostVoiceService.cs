using Microsoft.JSInterop;
using NaoComemoreAntesdoFim.Models;
using System.Text.Json.Serialization;

namespace NaoComemoreAntesdoFim.Services;

public class GameHostVoiceService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private readonly string _ttsApiBaseUrl;
    private IJSObjectReference? _module;
    private bool _initialized;
    private bool _supported;

    public bool IsEnabled { get; set; } = true;
    public bool ReadQuestions { get; set; } = true;

    public GameHostVoiceService(IJSRuntime jsRuntime, IConfiguration configuration)
    {
        _jsRuntime = jsRuntime;
        _ttsApiBaseUrl = configuration["TtsApi:BaseUrl"] ?? string.Empty;
    }

    public async Task<bool> EnsureInitializedAsync()
    {
        if (_initialized)
            return _supported;

        try
        {
            _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/gameHostSpeech.js");
            await _module.InvokeVoidAsync("configure", new { baseUrl = _ttsApiBaseUrl });
            _supported = await _module.InvokeAsync<bool>("isSupported");
            _initialized = true;
        }
        catch
        {
            _supported = false;
            _initialized = true;
        }

        return _supported;
    }

    public async Task<bool> IsAvailableAsync()
    {
        return await EnsureInitializedAsync() && _supported;
    }

    public async Task AnnounceWelcomeAsync()
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        await SpeakAsync(GameHostScripts.Welcome());
    }

    public async Task AnnounceAfterDrawAsync(GameEngineService game)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        var player = game.CurrentPlayer?.Name ?? "Jogador";
        var card = game.CurrentCard;
        if (card is null)
            return;

        if (GameEngineService.IsQuestionCard(card))
        {
            await SpeakAsync(GameHostScripts.QuestionDrawn(player));
            return;
        }

        if (game.SocialChallengePending)
        {
            await SpeakAsync(GameHostScripts.SocialChallenge(player, card.Text));
            return;
        }

        if (game.EffectApplied)
        {
            await AnnounceActionEffectAsync(game, targetName: null);
            return;
        }

        await SpeakAsync(GameHostScripts.ActionCardDrawn(player, card));
    }

    public async Task AnnounceQuestionRevealedAsync(Card card, string playerName)
    {
        if (!IsEnabled || !ReadQuestions || !await EnsureInitializedAsync() || !_supported)
            return;

        if (!GameEngineService.IsQuestionCard(card))
            return;

        var text = GameHostScripts.BuildQuestionParts(card.Id, card.Text, card.HasMultipleChoice);
        await SpeakSequenceAsync(text, questionMode: true);
    }

    public async Task AnnounceQuestionResultAsync(string playerName, bool correct, Card? card)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        if (correct)
        {
            await SpeakAsync(GameHostScripts.CorrectAnswer(playerName));
            return;
        }

        char? correctOption = card?.HasMultipleChoice == true ? card.CorrectOption : null;
        await SpeakAsync(GameHostScripts.WrongAnswer(playerName, correctOption));
    }

    public async Task AnnounceTimerWarningAsync(string playerName, int secondsLeft)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        await SpeakAsync(GameHostScripts.TimerWarning(playerName, secondsLeft));
    }

    public async Task AnnounceActionEffectAsync(GameEngineService game, string? targetName, string? secondaryTargetName = null)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        var player = game.CurrentPlayer?.Name ?? "Jogador";
        var card = game.CurrentCard;
        if (card is null)
            return;

        var amount = card.Amount > 0 ? card.Amount : 1;
        var code = card.ActionCode;

        if (NormalizeActionCode(code) == "DonateAllChocolates"
            && !string.IsNullOrEmpty(targetName)
            && !string.IsNullOrEmpty(secondaryTargetName))
        {
            await SpeakAsync(
                $"{targetName} doou todos os chocolates para {secondaryTargetName}. Não comemore antes do fim!");
            return;
        }

        await SpeakAsync(GameHostScripts.ActionEffect(player, code, targetName, amount));
    }

    public async Task AnnounceSocialChallengeDoneAsync(string playerName)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        await SpeakAsync(GameHostScripts.SocialChallengeDone(playerName));
    }

    public async Task AnnounceGameEndAsync(GameEngineService game)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        var champion = game.GetWinnerPlayer() ?? game.GetChocolateLeader();
        var message = champion is null
            ? GameHostScripts.GameEnd(null, 0, 0)
            : GameHostScripts.GameEnd(champion.Name, champion.Points, champion.Chocolates);

        await SpeakAsync(message);
    }

    public async Task RepeatCurrentAsync(GameEngineService game)
    {
        if (!IsEnabled || !await EnsureInitializedAsync() || !_supported)
            return;

        var card = game.CurrentCard;
        var player = game.CurrentPlayer?.Name ?? "Jogador";
        if (card is null)
            return;

        if (GameEngineService.IsQuestionCard(card) && game.CardRevealed && ReadQuestions)
        {
            await AnnounceQuestionRevealedAsync(card, player);
            return;
        }

        if (GameEngineService.IsQuestionCard(card))
        {
            await SpeakAsync(GameHostScripts.QuestionDrawn(player));
            return;
        }

        if (game.SocialChallengePending)
        {
            await SpeakAsync(GameHostScripts.SocialChallenge(player, card.Text));
            return;
        }

        await SpeakAsync(GameHostScripts.ActionCardDrawn(player, card));
    }

    public async Task StopAsync()
    {
        if (_module is null)
            return;

        try
        {
            await _module.InvokeVoidAsync("cancel");
        }
        catch
        {
            // Ignora falha ao cancelar fala.
        }
    }

    public class VoiceInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("lang")]
        public string Lang { get; set; } = string.Empty;

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
    }

    public class VoicePreferences
    {
        [JsonPropertyName("voiceName")]
        public string VoiceName { get; set; } = string.Empty;

        [JsonPropertyName("rate")]
        public double Rate { get; set; } = 1.0;

        [JsonPropertyName("pitch")]
        public double Pitch { get; set; } = 1.0;

        [JsonPropertyName("volume")]
        public double Volume { get; set; } = 1.0;
    }

    public async Task<List<VoiceInfo>> GetAvailableVoicesAsync()
    {
        if (!await EnsureInitializedAsync() || !_supported || _module is null)
            return new List<VoiceInfo>();

        try
        {
            return await _module.InvokeAsync<List<VoiceInfo>>("getAvailableVoices");
        }
        catch
        {
            return new List<VoiceInfo>();
        }
    }

    public async Task<VoicePreferences> GetPreferencesAsync()
    {
        if (!await EnsureInitializedAsync() || !_supported || _module is null)
            return new VoicePreferences();

        try
        {
            return await _module.InvokeAsync<VoicePreferences>("getVoicePreferences");
        }
        catch
        {
            return new VoicePreferences();
        }
    }

    public async Task UpdatePreferencesAsync(VoicePreferences prefs)
    {
        if (!await EnsureInitializedAsync() || !_supported || _module is null)
            return;

        try
        {
            await _module.InvokeVoidAsync("updateVoicePreferences", prefs);
        }
        catch
        {
            // Ignora erro ao atualizar preferências de voz.
        }
    }

    public async Task TestVoiceAsync(string sampleText)
    {
        if (!await EnsureInitializedAsync() || !_supported)
            return;

        await SpeakAsync(sampleText);
    }

    private static string NormalizeActionCode(string actionCode)
    {
        if (string.IsNullOrWhiteSpace(actionCode))
            return string.Empty;

        return actionCode.Split(':', StringSplitOptions.RemoveEmptyEntries)[0];
    }

    private async Task SpeakAsync(string text, bool questionMode = false)
    {
        if (_module is null || string.IsNullOrWhiteSpace(text))
            return;

        try
        {
            var rateMultiplier = questionMode ? 0.95 : 1.0;
            var pitchMultiplier = questionMode ? 0.98 : 1.0;
            var pauseMs = questionMode ? 480 : 350;

            await _module.InvokeAsync<bool>(
                "speakNaturally",
                text,
                rateMultiplier,
                pitchMultiplier,
                1.0,
                "pt-BR",
                pauseMs);
        }
        catch
        {
            // Falha de TTS não deve interromper o jogo.
        }
    }

    private async Task SpeakSequenceAsync(IReadOnlyList<string> parts, bool questionMode = false)
    {
        if (_module is null || parts.Count == 0)
            return;

        try
        {
            var rateMultiplier = questionMode ? 0.95 : 1.0;
            var pitchMultiplier = questionMode ? 0.98 : 1.0;
            var pauseMs = questionMode ? 520 : 380;

            await _module.InvokeAsync<bool>(
                "speakSequence",
                parts,
                rateMultiplier,
                pitchMultiplier,
                1.0,
                "pt-BR",
                pauseMs);
        }
        catch
        {
            // Falha de TTS não deve interromper o jogo.
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            await StopAsync();
            await _module.DisposeAsync();
        }
    }
}
