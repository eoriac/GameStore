using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;

[Route("api/users/{userId}/library")]
[ApiController]
public class UserLibraryController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IGameLibraryRepository gameLibraryRepository;
    private readonly IMapper mapper;

    public UserLibraryController(
        IUserRepository userRepository,
        IGameLibraryRepository gameLibraryRepository,
        IMapper mapper
        )
    {
        this.userRepository = userRepository;
        this.gameLibraryRepository = gameLibraryRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Library>>> GetUserLibraryAsync(string userId)
    {
        var libraryItemsCursor = await this.gameLibraryRepository.GetLibraryGamesAsync(userId);

        return Ok(libraryItemsCursor.ToList());
    }
    
    [HttpGet("{id}", Name = "GetLibraryGame")]
    public async Task<ActionResult<Library>> GetUserLibraryGameAsync(string userId, string id)
    {
        var libraryItemsCursor = await this.gameLibraryRepository.GetLibraryGameAsync(userId, id);

        return Ok(libraryItemsCursor);
    }

    [Authorize(Policy = "OwnGame")]
    [HttpPost]
    public ActionResult<Library> PostUserLibraryGame(string userId, Library library)
    {
        library.UserId = userId;
        this.gameLibraryRepository.AddLibrary(library);

        return CreatedAtRoute("GetLibraryGame", new { userId = userId, id = library.Id }, library);
    }

    [Authorize(Policy = "OwnGame")]
    [HttpDelete("{id}")]
    public ActionResult DeleteUserLibraryGame(string userId, string id)
    {
        return Ok();
    }
}
