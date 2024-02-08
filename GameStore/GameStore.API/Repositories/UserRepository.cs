
namespace GameStore.API;

public class UserRepository : IUserRepository
{
    public Task DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Game>> GetUserGamesAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }
}
