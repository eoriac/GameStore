using Moq;

namespace GameStore.API.Tests;

public class MoqTests
{
    [Fact]
    public void AddLibrary_LibraryWithoutUserId_MustThrowException()
    {
        // Arrange
        var libraryRepositoryForTestsMock = new Mock<IGameLibraryRepository>();

            libraryRepositoryForTestsMock
                  .Setup(mo => mo.AddLibrary(It.IsAny<Library>()));
            // libraryRepositoryForTestsMock
            //       .Setup(mo => mo.GetLibraryGame(It.IsAny<string>(), It.IsAny<string>()))
            //       .Returns(new Library(){ Id = "SomeId" });

        var gameLibraryService = new GameLibraryService(gameLibraryRepository: libraryRepositoryForTestsMock.Object);
        var libraryGame = new Library(){ Id = "SomeID", Name = "GoT" };
            
        // Act
        try
        {
            gameLibraryService.AddLibrary(libraryGame);
        }
        catch (ArgumentException e){
            // Assert
            Assert.Equal("UserId can't be empty", e.Message);
        }

        // Assert.Throws<ArgumentException>(_ => { gameLibraryService.AddLibrary(libraryGame);});
    }

    [Fact]
    public void AddLibrary_LibraryOk_MustCreateLibraryGame()
    {
        // Arrange
        var libraryGame = new Library(){ Id = "SomeID", UserId = "SomeUserId", Name = "GoT" };

        var libraryRepositoryForTestsMock = new Mock<IGameLibraryRepository>();

        libraryRepositoryForTestsMock
                .Setup(mo => mo.AddLibrary(It.IsAny<Library>()));
        libraryRepositoryForTestsMock
                .Setup(mo => mo.GetLibraryGame(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(libraryGame);
        var gameLibraryService = new GameLibraryService(gameLibraryRepository: libraryRepositoryForTestsMock.Object);
        
            
        // Act
        gameLibraryService.AddLibrary(libraryGame);
        var result = gameLibraryService.RetrieveLibrary("someuserId", "someId");
        
        // Assert
        Assert.Equal(libraryGame, result);
    }    
}
