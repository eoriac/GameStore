
namespace GameStore.API.Tests;

public class GameLibraryRespositoryForTests() : IGameLibraryRepository
{

    private List<Library> libraryCollection = new List<Library>();

    public void AddLibrary(Library libraryGame)
    {
        return;
    }

    public Library? GetLibraryGame(string userId, string gameId)
    {
        return new Library(){ Id = "SomeId"};
    }

    public Task<Library?> GetLibraryGameAsync(string userId, string gameId)
    {
        throw new NotImplementedException();
    }

    public IList<Library> GetLibraryGames(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Library>> GetLibraryGamesAsync(string userId)
    {
        throw new NotImplementedException();
    }
}