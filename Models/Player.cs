namespace NaoComemoreAntesdoFim.Models;

public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Chocolates { get; set; }
    public int Points { get; set; }
}
