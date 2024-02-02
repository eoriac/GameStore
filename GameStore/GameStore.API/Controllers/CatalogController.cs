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
    public IActionResult Post(Game game)
    {
        this.gameRepository.AddGame(game);
        return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
    }
}
