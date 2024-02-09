
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameStore.API;

public class UserRepository(
    ILogger<UserRepository> logger,
    IMongoDatabase mongoDatabase, 
    IOptions<GameStoreDatabaseSettings> options) : IUserRepository

{
    private readonly IMongoCollection<Game> gameCollection = mongoDatabase.GetCollection<Game>(options.Value.UsersCollection);
    private readonly ILogger<UserRepository> logger = logger;

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
