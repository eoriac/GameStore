using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameStore.API;

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="mongoDatabase"></param>
/// <param name="options"></param>
public class GameLibraryRepository
(
    ILogger<GameLibraryRepository> logger,
    IMongoDatabase mongoDatabase, 
    IOptions<GameStoreDatabaseSettings> options) : IGameLibraryRepository
{
    private readonly IMongoCollection<Library> libraryCollection = mongoDatabase.GetCollection<Library>(options.Value.UserGamesCollection);
    private readonly ILogger<GameLibraryRepository> logger = logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    public Library? GetLibraryGame(string userId, string gameId)
    {
        var result = libraryCollection.Find(game => game.UserId == userId && game.Id == gameId).FirstOrDefault();

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="gameId"></param>
    /// <returns></returns>
    public async Task<Library?> GetLibraryGameAsync(string userId, string gameId)
    {
        var resultCursor = await libraryCollection.FindAsync(game => game.UserId == userId && game.Id == gameId);

        var result = resultCursor.FirstOrDefault();

        return result;
    }    

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IList<Library> GetLibraryGames(string userId)
    {
        var results = libraryCollection.Find(gm => gm.UserId == userId).ToList();

        return results;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="libraryGame"></param>
    public void AddLibrary(Library libraryGame)
    {
        if (string.IsNullOrWhiteSpace(libraryGame.UserId) == true){
            this.logger.LogError("UserId can't be empty");
            throw new ArgumentException("UserId can't be empty");
        }

        libraryCollection.InsertOne(libraryGame);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IList<Library>> GetLibraryGamesAsync(string userId)
    {
        var gamesCursor = await libraryCollection.FindAsync(gm => gm.UserId == userId);

        var results = gamesCursor.ToList();

        return results;
    }
}
