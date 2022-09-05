using Xunit;
using Moq;
using SleekBoard.Controllers;
using SleekBoard.Repositories;
using AutoMapper;
using SleekBoard.Entities;
using SleekBoard.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace SleekBoard.Test;

public class BoardControllerTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;
    private readonly Mock<IBoardItemRepository> _boardItemRepositoryMock;
    private readonly IMapper _mapper;

    public BoardControllerTest()
    {
        _boardRepositoryMock = new Mock<IBoardRepository>();
        _boardItemRepositoryMock = new Mock<IBoardItemRepository>();     

        var config = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Board, BoardOutput>();
            cfg.CreateMap<BoardItem, BoardItemOutput>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async void GetBoards_NoFilteringAndNoSorting_ReturnBoards()
    {
        // Arrange

        var fakeBoards = new List<Board>
        {
            new Board("Fake Name")
        };

        var fakeInput = new BoardQueryInput();

        _boardRepositoryMock.Setup(x => x.GetAllAsync(null, null, null))
            .Returns(Task.FromResult(fakeBoards));
        
        // Act

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _boardItemRepositoryMock.Object,
            _mapper);

        var result = await boardController.GetBoards(fakeInput);

        // Assert

        var dtos = new List<BoardOutput>
        {
            new BoardOutput 
            {
                Name = "Fake Name"
            }
        };

        Assert.Equal(dtos.First().Name, result.Value!.ToList().First().Name);
    }

    [Fact]
    public async void GetBoards_FilterByName_ReturnBoards()
    {
        // Arrange

        var fakeBoards = new List<Board>
        {
            new Board("Fake Name")
        };

        var fakeInput = new BoardQueryInput
        {
            Name = "Fake"
        };

        _boardRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<string>(), null, null))
            .Returns(Task.FromResult(fakeBoards));
        
        // Act

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _boardItemRepositoryMock.Object,
            _mapper);

        var result = await boardController.GetBoards(fakeInput);

        // Assert

        var dtos = new List<BoardOutput>
        {
            new BoardOutput 
            {
                Name = "Fake Name"
            }
        };

        Assert.Equal(dtos.First().Name, result.Value!.ToList().First().Name);
    }

    [Fact]
    public async void GetBoard_CorrectBoardId_ReturnBoard()
    {
        // Arrange

        var fakeBoard = new Board("Fake Name");

        var boardId = Guid.NewGuid();

        _boardRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult(fakeBoard));
        
        // Act

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _boardItemRepositoryMock.Object,
            _mapper);

        var result = await boardController.GetBoard(boardId);

        // Assert

        var dto = new BoardOutput 
        {
            Name = "Fake Name"
        };

        Assert.Equal(dto.Name, result.Value!.Name);
    }

    [Fact]
    public async void GetBoard_WrongBoardId_ReturnNotFound()
    {
        // Arrange

        var boardId = Guid.NewGuid();

        _boardRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult((Board)null!));
        
        // Act

        var boardController = new BoardController(
            _boardRepositoryMock.Object,
            _boardItemRepositoryMock.Object,
            _mapper);

        var result = (await boardController.GetBoard(boardId)).Result as NotFoundResult;

        // Assert

        Assert.NotNull(result);
    }
}