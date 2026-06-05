namespace NaoComemoreAntesdoFim.Models;

public class Card
{
    public int Id { get; set; }
    public CardType Type { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? Answer { get; set; }
    public string ActionCode { get; set; } = string.Empty;
    /// <summary>Quantidade usada por cartas de ação/recompensa/penalidade (ex.: chocolates).</summary>
    public int Amount { get; set; }

    // Múltipla escolha (apenas cartas de pergunta)
    public string? OptionA { get; set; }
    public string? OptionB { get; set; }
    public string? OptionC { get; set; }
    /// <summary>'A', 'B' ou 'C' — letra da opção correta.</summary>
    public char CorrectOption { get; set; }

    public bool HasMultipleChoice =>
        !string.IsNullOrEmpty(OptionA) &&
        !string.IsNullOrEmpty(OptionB) &&
        !string.IsNullOrEmpty(OptionC);
}
