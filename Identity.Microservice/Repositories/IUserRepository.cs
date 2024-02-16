
public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync();

    public Task<User> GetUserAsync(string id);

    public Task DeleteUserAsync(string id);

    public Task<ICollection<User>> GetUserGamesAsync(string userId);
}
