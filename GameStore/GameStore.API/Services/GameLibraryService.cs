namespace GameStore.API;

public class GameLibraryService(
    // ILogger<GameLibraryService> logger,
    IGameLibraryRepository gameLibraryRepository) : IGameLibraryService
{
    private readonly IGameLibraryRepository gameLibraryRepository = gameLibraryRepository;
    // private readonly ILogger<GameLibraryService> logger = logger;

    public void AddLibrary(Library libraryGame)
    {
        
        if (string.IsNullOrWhiteSpace(libraryGame.UserId) == true){
            // this.logger.LogError("UserId can't be empty");
            throw new ArgumentException("UserId can't be empty");
        }


        this.gameLibraryRepository.AddLibrary(libraryGame);
    }

    public Library RetrieveLibrary(string userId, string id)
    {
        return this.gameLibraryRepository.GetLibraryGame(userId, id);
    }    
}
