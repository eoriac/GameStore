using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;

[Authorize]
[ApiController]
[Route("api/catalog")]
// [Route("api/v{version:apiVersion}/catalog")]
public class CatalogController(IGameRepository gameRepository, IMapper mapper) : ControllerBase
{
    private readonly IGameRepository gameRepository = gameRepository;
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// Retrieve all games
    /// </summary>
    /// <returns>Pagination with games</returns>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        var games = this.mapper.Map<IList<Game>>(this.gameRepository.GetGames());

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

    [Authorize(Roles = "GoldUser")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post(GameForCreateDto gameForCreate)
    {
        // var game = new Game
        // {
        //     Name = gameForCreate.Name,
        //     Description = gameForCreate.Description,
        //     Price = gameForCreate.Price
        // };

        var game = this.mapper.Map<GameForCreateDto, Game>(gameForCreate);

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

        // game = this.mapper.Map<Game>(gameForUpdate);

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
