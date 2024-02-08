using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;

[Authorize]
[ApiController]
[Route("api/catalog")]
public class CatalogController(IGameRepository gameRepository) : ControllerBase
{
    private readonly IGameRepository gameRepository = gameRepository;

    /// <summary>
    /// Retrieve all games
    /// </summary>
    /// <returns>Pagination with games</returns>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        var games = this.gameRepository.GetGames();

        return Ok(games);
    }

    /// <summary>
    /// Retrieve one game based on ID
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <returns>Game or not found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(string id)
    {
        var game = this.gameRepository.GetGame(id);

        if (game == null){
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post(GameForCreateDto gameForCreate)
    {
        var game = new Game
        {
            Name = gameForCreate.Name,
            Description = gameForCreate.Description,
            Price = gameForCreate.Price
        };

        this.gameRepository.AddGame(game);
        
        return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public IActionResult Put(string id, GameForUpdateDto gameForUpdate)
    {
        var game = this.gameRepository.GetGame(id);

        if (game == null)
        {
            return NotFound();
        }

        game.Name = gameForUpdate.Name;
        game.Description = gameForUpdate.Description;
        game.Price = gameForUpdate.Price;

        this.gameRepository.UpdateGame(game);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {        
        var game = this.gameRepository.GetGame(id);

        if (game == null){
            return NotFound();
        }

        this.gameRepository.DeleteGame(id);

        return NoContent();
    }    
}
