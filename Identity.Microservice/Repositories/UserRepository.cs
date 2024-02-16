
using Identity.Microservice;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class UserRepository(
    ILogger<UserRepository> logger,
    IMongoDatabase mongoDatabase, 
    IOptions<IdentityMicroserviceDbSettings> options) : IUserRepository

{
    private readonly IMongoCollection<User> gameCollection = mongoDatabase.GetCollection<User>(options.Value.UsersCollection);
    private readonly ILogger<UserRepository> logger = logger;

    public Task DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetUserGamesAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }
}
