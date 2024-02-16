namespace GameStore.API;

public class UserDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string? AvatarUrl { get; set; }

    public int NumberOfGames
    {
        get
        {
            return this.GamesLibrary.Count;
        }
    }

    public ICollection<GameDto> GamesLibrary { get; set; } = [];
}
