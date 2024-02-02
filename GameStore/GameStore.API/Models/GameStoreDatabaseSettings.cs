namespace GameStore.API;

public class GameStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string GamesCollection { get; set; } = null!;
}
