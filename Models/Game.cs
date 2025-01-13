namespace GameApi.Models;

public class Game
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public double Price { get; set; }
}
