using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;

[ApiController]
[Route("api/catalog")]
public class CatalogController(IGameRepository gameRepository) : ControllerBase
{
    private readonly IGameRepository gameRepository = gameRepository;

    [HttpGet]
    public IActionResult Get()
    {
        var games = this.gameRepository.GetGames();

        return Ok(games);
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var game = this.gameRepository.GetGame(id);

        return Ok(game);
    }

    [HttpPost]
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
