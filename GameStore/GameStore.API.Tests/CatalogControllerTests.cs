using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GameStore.API.Tests;

public class CatalogControllerTests
{
    [Fact]
    public void CreateGame_ValidGameDto_MustReturnProperActionResult()
    {
        // Arrange
        var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock.Setup(m => m.AddGame(It.IsAny<Game>()));

        var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<GameForCreateDto, Game>(It.IsAny<GameForCreateDto>())) // stubs
                .Returns(new Game(){  Name = "SomeGame", Description = "SomeDescription", Id = "SomeId"});

        var underTestController = new CatalogController(gameRepositoryMock.Object, mapperMock.Object);
        var newGameDto = new GameForCreateDto(){ Name = "NewGame", Description = "New Game for Tests" };

        // Act
        var result = underTestController.Post(newGameDto);

        // Assert
        var assertActionResult = Assert.IsType<ActionResult<Game>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(assertActionResult.Result);
        Assert.Equal("Get", createdAtActionResult.ActionName);
    }

    [Fact]
    public void CreateGame_ValidGameDto_MustCallAddGame()
    {
        // Arrange
        var gameForCreate = new Game(){  Name = "SomeGame", Description = "SomeDescription", Id = "someID"};

        var gameRepositoryMock = new Mock<IGameRepository>();
            gameRepositoryMock
                .Setup(m => m.AddGame(It.Is<Game>(gm => gm.Id == "bla")))
                .Verifiable();

        var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<GameForCreateDto, Game>(It.IsAny<GameForCreateDto>())) // stubs
                .Returns(gameForCreate);

        var underTestController = new CatalogController(gameRepositoryMock.Object, mapperMock.Object);
        var newGameDto = new GameForCreateDto(){ Name = "NewGame", Description = "New Game for Tests" };
        

        // Act
        var result = underTestController.Post(newGameDto);

        // Assert
        var assertActionResult = Assert.IsType<ActionResult<Game>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(assertActionResult.Result);
        Assert.Equal("Get", createdAtActionResult.ActionName);
    }
}
