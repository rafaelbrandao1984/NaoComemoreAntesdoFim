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
}
