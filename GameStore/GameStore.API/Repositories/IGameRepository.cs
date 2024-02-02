namespace GameStore.API;

public interface IGameRepository
{
    IList<Game> GetGames();

    Game? GetGame(string gameId);

    void AddGame(Game game);

    void UpdateGame(Game game);

    void DeleteGame(string gameId);
}

/*
public interface IGenericRepository<T>
{
    IList<T> GetElements();

    T? GetElement(string elementId);

    void AddElement(T element);

    void UpdateElement(T element);

    void DeleteElement(string elementId);
}
*/