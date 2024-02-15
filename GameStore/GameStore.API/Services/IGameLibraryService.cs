namespace GameStore.API;

public interface IGameLibraryService
{
    void AddLibrary(Library libraryGame);

    Library RetrieveLibrary(string userId, string id);
}