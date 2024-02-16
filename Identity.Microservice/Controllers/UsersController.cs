using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 
/// </summary>
/// <param name="userRepository"></param>
/// <param name="mapper"></param>
[Route("api/users")]
[Authorize]
[ApiController]
public class UsersController(
    IUserRepository userRepository,
    IMapper mapper) : ControllerBase
{
    private readonly IUserRepository userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper mapper = mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        //var claims = User.Claims.ToList();

        var usersFromDb = await userRepository.GetUsersAsync();

        var usersForResult = mapper.Map<IEnumerable<UserDto>>(usersFromDb);

        return Ok(usersForResult);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var userFromDb = await userRepository.GetUserAsync(id);

        if (userFromDb == null)
        {
            return NotFound();
        }

        var userForResult = mapper.Map<UserDto>(userFromDb);

        return Ok(userForResult);
    }
}
