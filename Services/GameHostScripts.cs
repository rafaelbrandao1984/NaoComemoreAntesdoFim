using NaoComemoreAntesdoFim.Models;

namespace NaoComemoreAntesdoFim.Services;

/// <summary>Frases do apresentador virtual — placeholders: {nome}, {alvo}, {pontos}, {chocolates}, {numero}, {texto}.</summary>
public static class GameHostScripts
{
    private static readonly Random Random = new();

    public static string Pick(params string[] options) =>
        options[Random.Next(options.Length)];

    public static string Welcome() =>
        Pick(
            "Olá! Eu sou o guardião do chocolate. Não comemore antes do fim! Boa partida a todos.",
            "Atenção, competidores! Eu vou lembrar: chocolate na mesa não é vitória. Não comemore antes do fim!",
            "Prontos? Eu fico de olho em quem comemora cedo. Vamos começar!");

    public static string QuestionDrawn(string playerName) =>
        Pick(
            $"{playerName}, sorteou uma pergunta! Vire a carta quando estiver pronto.",
            $"Vez de {playerName}! Tem pergunta na mesa. Vire a carta, mas não comemore ainda.",
            $"{playerName}, prepare-se! Uma pergunta te espera. Vire a carta.");

    public static IReadOnlyList<string> BuildQuestionParts(int questionNumber, string questionText, bool hasMultipleChoice)
    {
        var parts = new List<string>
        {
            $"Pergunta número {questionNumber}.",
            questionText
        };

        if (hasMultipleChoice)
            parts.Add("Escolha A, B ou C na tela.");

        return parts;
    }

    public static string CorrectAnswer(string playerName) =>
        Pick(
            $"Acertou, {playerName}! Ganhou chocolate e pontos. Guarde aí — não coma! E não comemore antes do fim!",
            $"Opa, {playerName}! Resposta certa! Mais um chocolate na sua pilha. Proibido comer. Proibido comemorar!",
            $"{playerName} mandou bem! Chocolate e pontos pra você. A caixa está de olho — não comemore antes do fim!",
            $"Isso aí, {playerName}! Acertou! Chocolate na mão, celebração só no final!");

    public static string WrongAnswer(string playerName, char? correctOption) =>
        correctOption is { } letter
            ? Pick(
                $"{playerName}, desta vez não deu. A correta era a opção {letter}. Sem chocolate agora.",
                $"Quase, {playerName}! A resposta certa era a opção {letter}. Na próxima vai.",
                $"{playerName}, errou desta vez. Era a opção {letter}. Seguimos em frente!")
            : Pick(
                $"{playerName}, desta vez não deu. Sem chocolate agora.",
                $"Quase, {playerName}! Na próxima você acerta.",
                $"{playerName}, errou desta vez. A partida continua!");

    public static string TimerWarning(string playerName, int seconds) =>
        Pick(
            $"{playerName}, tic tac! Restam {seconds} segundos!",
            $"Corre, {playerName}! Só {seconds} segundos!",
            $"O relógio não perdoa, {playerName}! {seconds} segundos!");

    public static string ActionCardDrawn(string playerName, Card card)
    {
        var kind = card.Type switch
        {
            CardType.Reward => "recompensa",
            CardType.Penalty => "penalidade",
            _ => "ação"
        };

        return Pick(
            $"{playerName}, carta de {kind}! {card.Text} Vire a carta.",
            $"Opa, {playerName}! Veio carta de {kind}. {card.Text}",
            $"{playerName}, olha só — carta de {kind}! {card.Text} Não comemore antes do fim!");
    }

    public static string SocialChallenge(string playerName, string cardText) =>
        Pick(
            $"{playerName}, desafio na mesa! {cardText} Façam na roda e confirmem na tela.",
            $"Hora do show, {playerName}! {cardText} Todo mundo assistindo!",
            $"{playerName}, carta especial! {cardText} Sem consulta, hein!");

    public static string SocialChallengeDone(string playerName) =>
        Pick(
            $"Desafio cumprido! {playerName}, pode passar a vez.",
            $"Muito bem, {playerName}! Desafio feito. Próximo!",
            $"Show! {playerName} cumpriu a carta. Seguimos!");

    public static string ActionEffect(string playerName, string actionCode, string? targetName, int amount)
    {
        var code = actionCode.Split(':', StringSplitOptions.RemoveEmptyEntries)[0];

        return code switch
        {
            "GiveChocolates" or "GainChocolate" =>
                Pick(
                    $"Opa, {playerName}! Ganhou {amount} chocolate{(amount > 1 ? "s" : "")}! Não coma! Não comemore antes do fim!",
                    $"{playerName} levou {amount} chocolate{(amount > 1 ? "s" : "")}! Guarda aí — comemoração só no final!",
                    $"Chocolate pra {playerName}! {amount} unidade{(amount > 1 ? "s" : "")}. Proibido comer agora!"),

            "EatChocolate" =>
                Pick(
                    $"Que delícia, {playerName}! Pode comer {amount} chocolate{(amount > 1 ? "s" : "")} — desta vez a carta mandou!",
                    $"{playerName}, a carta liberou! Coma {amount} chocolate{(amount > 1 ? "s" : "")} — raridade na mesa!",
                    $"Permissão especial! {playerName} pode comer {amount} chocolate{(amount > 1 ? "s" : "")} agora!"),

            "LoseAllChocolates" when !string.IsNullOrEmpty(targetName) =>
                $"Ah não! {targetName} devolveu todos os chocolates. A caixa agradece.",

            "LoseAllChocolates" =>
                Pick(
                    $"{playerName}, devolveu todos os chocolates! A caixa sorriu.",
                    $"Ops, {playerName}! Todos os chocolates voltaram. Não comemore antes do fim — agora com motivo!"),

            "StealAllChocolates" when !string.IsNullOrEmpty(targetName) =>
                Pick(
                    $"{playerName} ficou com todos os chocolates de {targetName}. {targetName}, calma — ainda tem jogo!",
                    $"Mudança no placar! {playerName} recebeu os chocolates de {targetName}. Não comemore antes do fim!"),

            "StealChocolate" when !string.IsNullOrEmpty(targetName) =>
                $"{playerName} recebeu {amount} chocolate{(amount > 1 ? "s" : "")} de {targetName}.",

            "GiveAllChocolates" when !string.IsNullOrEmpty(targetName) =>
                $"{playerName} passou todos os chocolates para {targetName}! Generosidade ou estratégia?",

            "DonateAllChocolates" when !string.IsNullOrEmpty(targetName) =>
                $"Doação na mesa! Chocolates transferidos entre os jogadores.",

            "DrinkWater" or "DrinkWaterSelf" =>
                Pick(
                    $"Água na mesa! {playerName}, hidratação é importante.",
                    $"{playerName}, copo d'água! Saúde em primeiro lugar."),

            "ReciteScripture" =>
                $"{playerName}, hora de citar uma escritura! Sem consulta!",

            "SingHymn" =>
                $"{playerName}, solta a voz! Hino favorito na roda!",

            _ =>
                $"{playerName}, efeito aplicado! Sigam o jogo."
        };
    }

    public static string GameEnd(string? championName, int points, int chocolates) =>
        championName is null
            ? Pick(
                "Acabou a partida! Agora sim — podem comemorar!",
                "Fim de jogo! A tortura acabou — comam os chocolates!")
            : Pick(
                $"Acabou! Agora sim podem comemorar! Quem mais pontuou: {championName}, com {points} pontos e {chocolates} chocolates!",
                $"Fim de jogo! {championName} liderou com {points} pontos e {chocolates} chocolates. Agora sim — comemorem!",
                $"Terminou! Campeão: {championName}! {points} pontos, {chocolates} chocolates. Não comemore antes do fim… ops, agora pode!");

    public static string PlacarAfterCorrect(string playerName, int points, int chocolates) =>
        Pick(
            $"{playerName} subiu no placar! {points} pontos e {chocolates} chocolate{(chocolates == 1 ? "" : "s")} — guarda tudo, hein!",
            $"Placar atualizado! {playerName} tem {points} pontos. A tentação cresce, a regra continua!",
            $"{playerName} encheu o bolso simbólico: {chocolates} chocolate{(chocolates == 1 ? "" : "s")} e {points} pontos. Não coma!",
            $"Mais pontos para {playerName}! {points} no total. Comemoração continua proibida!",
            $"A roda viu: {playerName} mandou bem e levou chocolate. Só não comemore antes do fim!");

    public static string PlacarAfterWrong(string playerName) =>
        Pick(
            $"{playerName} errou, mas o jogo segue! Quem lidera ainda não pode comer.",
            $"Desta vez não deu para {playerName}. A caixa de chocolate permanece vigilante.",
            $"{playerName} ficou na saudade do chocolate. Próxima carta pode virar!",
            $"Errou, {playerName}! Sem drama — a diversão é não comemorar cedo demais.",
            $"Placar quieto para {playerName} agora. A roda inteira está de olho!");

    public static string PlacarAfterSteal(string thiefName, string victimName) =>
        Pick(
            $"Plot twist! {thiefName} levou chocolate de {victimName}. {victimName}, respira — ainda tem jogo!",
            $"Roubo autorizado na mesa! {thiefName} ficou mais rico e {victimName} mais magoado.",
            $"{victimName} perdeu chocolate para {thiefName}! Não comemore antes do fim — especialmente agora!",
            $"Mudança brusca no placar: {thiefName} agradece, {victimName} faz cara de drama.",
            $"Chocolate trocou de dono! {thiefName} sorri, {victimName} conta até dez.");

    public static string PlacarLeaderSpotlight(string leaderName, int points, int chocolates) =>
        Pick(
            $"{leaderName} lidera com {points} pontos e {chocolates} chocolate{(chocolates == 1 ? "" : "s")}! Proibido comer!",
            $"Atenção: {leaderName} está na frente — {points} pontos! A tentação é real, a regra também.",
            $"{leaderName} domina o placar! {chocolates} chocolate{(chocolates == 1 ? "" : "s")} à vista. Não comemore!",
            $"Olhem o placar: {leaderName} com {points} pontos. Quem comemorar cedo leva bronca da roda!",
            $"{leaderName} no topo! Chocolate na mesa, boca fechada até o fim!");

    public static string ChampionFunTitle(string name) =>
        Pick(
            $"Rei do Chocolate Proibido: {name}",
            $"Campeão da Autocontrole: {name}",
            $"Mestre do Não Comemore: {name}",
            $"Lenda da Roda: {name}",
            $"Coroa de Ouro (e chocolate): {name}");

    public static string SecondPlaceFunTitle(string name) =>
        Pick(
            $"Vice do Drama: {name}",
            $"Quase Campeão: {name}",
            $"Prata Brilhante: {name}");

    public static string ThirdPlaceFunTitle(string name) =>
        Pick(
            $"Bronze Determinado: {name}",
            $"Terceiro com Estilo: {name}",
            $"Pódio Garantido: {name}");
}
