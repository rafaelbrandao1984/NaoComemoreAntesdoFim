namespace NaoComemoreAntesdoFim.Models;

/// <summary>Regras do cartão (página 31 do material impresso), adaptadas ao app.</summary>
public static class GameRules
{
    public const int MaxGameDurationHours = 2;
    public const int AnswerTimeLimitSeconds = 60;
    public const int ChocolatesPerCorrectAnswer = 1;
    public const int PointsPerCorrectAnswer = 5;

    public const string MainRule = "REGRA PRINCIPAL: DIVIRTAM-SE!";

    public static readonly string[] Items =
    [
        "Siga rigorosamente os comandos das cartas.",
        "Acabar o chocolate da caixa do jogo não significa fim de jogo.",
        "Cada resposta correta vale 1 chocolate e 5 pontos.",
        "Não coma nenhum chocolate seu, a não ser que o próprio jogo mande.",
        "Os chocolates devem ficar à vista de todos os competidores.",
        "Cada pergunta tem no máximo 1 minuto para ser respondida.",
        "Os sabores de chocolate podem ser escolhidos, mas com rapidez para não atrapalhar o jogo.",
        "É preferível que os jogadores se organizem em roda.",
        "Cada jogador deve ler a própria pergunta ao retirá-la da urna.",
        "Toda prenda ou tarefa deve ser feita na presença de todos os competidores.",
        "Não há limite de prendas: cada competidor deve cumprir a carta quantas vezes for necessário.",
        "Se sobrar chocolate na caixa quando acabarem as cartas de pergunta, divida entre todos.",
        "Não pesquise a resposta antes de responder.",
        "O jogo termina quando acabarem as cartas de pergunta, quando o tempo da partida chegar ao fim ou após no máximo 2 horas de jogo.",
    ];

    public static readonly string CardProcedure =
        "Ao retirar a carta, antes de ler a pergunta, diga o número da carta (ex.: \"Pergunta nº 1\"). " +
        "Depois de responder, confira no gabarito somente o número correspondente à sua carta.";

    public static string FormatDurationHint(int? sessionMinutes) =>
        sessionMinutes is > 0
            ? $"Partida com limite de {sessionMinutes} min (máximo {MaxGameDurationHours} h)."
            : $"Partida até acabarem as perguntas ou no máximo {MaxGameDurationHours} h.";
}
