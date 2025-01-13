using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameApi.Models;

namespace GameApi.Controllers;

[Route("api/games")]
[ApiController]
public class GamesController(GameContext context) : ControllerBase
{
    // GET: api/games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
    {
        return await context.Games.Select(x => ItemToDTO(x)).ToListAsync();
    }

    // GET: api/games/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDTO>> GetGame(long id)
    {
        var game = await context.Games.FindAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return ItemToDTO(game);
    }

    // PUT: api/games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(long id, Game game)
    {
        if (id != game.Id)
        {
            return BadRequest();
        }

        context.Entry(game).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GameExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Games
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Game>> PostGame(Game game)
    {
        context.Games.Add(game);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetGame", new { id = game.Id }, ItemToDTO(game));
    }

    // DELETE: api/games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(long id)
    {
        var game = await context.Games.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }

        context.Games.Remove(game);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool GameExists(long id)
    {
        return context.Games.Any(e => e.Id == id);
    }

    private static GameDTO ItemToDTO(Game game) =>
       new()
       {
           Id = game.Id,
           Title = game.Title,
           Price = game.Price
       };
}
