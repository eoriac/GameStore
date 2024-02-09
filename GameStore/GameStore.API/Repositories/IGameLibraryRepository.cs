namespace GameStore.API;

public interface IGameLibraryRepository
{
    Library? GetLibraryGame(string userId, string gameId);

    Task<Library?> GetLibraryGameAsync(string userId, string gameId);

    IList<Library> GetLibraryGames(string userId);

    void AddLibrary(Library libraryGame);

    Task<IList<Library>> GetLibraryGamesAsync(string userId);
}