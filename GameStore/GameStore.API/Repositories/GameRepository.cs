
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameStore.API;

public class GameRepository(
    // ILogger<GameRepository> logger,
    IMongoDatabase mongoDatabase, 
    IOptions<GameStoreDatabaseSettings> options) : IGameRepository
{
    private readonly IMongoCollection<Game> gameCollection = mongoDatabase.GetCollection<Game>(options.Value.GamesCollection);
    // private readonly ILogger<GameRepository> logger = logger;

    public Game? GetGame(string gameId)
    {
        var result = gameCollection.Find(game => game.Id == gameId).FirstOrDefault();

        return result;
    }

    public IList<Game> GetGames()
    {
        var results = gameCollection.Find(_ => true).ToList();

        return results;
    }

    public void AddGame(Game game)
    {
        gameCollection.InsertOne(game);
    }

    public void DeleteGame(string gameId)
    {
        gameCollection.DeleteOne(game => game.Id == gameId);
    }

    public void UpdateGame(Game game)
    {
        var updateResult = gameCollection.UpdateOne(gm => gm.Id == game.Id, Builders<Game>.Update
            .Set(gm => gm.Name, game.Name)
            .Set(gm => gm.Description, game.Description)
            .Set(gm => gm.Price, game.Price));

        // this.logger.LogDebug(updateResult.ModifiedCount.ToString());
    }
}
