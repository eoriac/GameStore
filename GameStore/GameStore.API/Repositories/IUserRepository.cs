namespace GameStore.API;


public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync();

    public Task<User> GetUserAsync(string id);

    public Task DeleteUserAsync(string id);

    public Task<ICollection<Game>> GetUserGamesAsync(string userId);
}
