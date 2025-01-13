using Microsoft.EntityFrameworkCore;

namespace GameApi.Models;

public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; } = null!;
}
