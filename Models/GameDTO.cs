namespace GameApi.Models;

public class GameDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
}
