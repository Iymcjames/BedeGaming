using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace LotteryGame.Tests;

public class ProgramTests
{
    private readonly Mock<IUserInteractionService> _mockUserInteractionService;
    private readonly Mock<ILotteryService> _mockLotteryService;

    public ProgramTests()
    {
        _mockUserInteractionService = new Mock<IUserInteractionService>();
        _mockLotteryService = new Mock<ILotteryService>();
    }

    [Fact]
    public async Task Program_Should_Add_Player_And_Distribute_Prizes()
    {
        // Arrange
        var player1 = new Player(1, 10);
        _mockUserInteractionService.Setup(x => x.GetPlayerTicketInputAsync()).ReturnsAsync(3);
        _mockLotteryService.Setup(x => x.AddPlayerAsync(player1)).Returns(Task.CompletedTask);
        _mockLotteryService.Setup(x => x.CreateOtherPlayersAsync(9, 14)).Returns(Task.CompletedTask);
        _mockLotteryService.Setup(x => x.GetPlayersAsync()).ReturnsAsync(new List<Player> { player1 });
        _mockLotteryService.Setup(x => x.DistributePrizesAsync()).Returns(Task.CompletedTask);
        _mockUserInteractionService.Setup(x => x.ShowPlayersTicketInfoAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_mockUserInteractionService.Object)
            .AddSingleton(_mockLotteryService.Object)
            .BuildServiceProvider();

        var lotteryService = serviceProvider.GetService<ILotteryService>();
        var userInteractionService = serviceProvider.GetService<IUserInteractionService>();

        // Act
        await userInteractionService.ShowGameEntryInformationAsync();
        player1.BuyTickets(await userInteractionService.GetPlayerTicketInputAsync());
        await lotteryService.AddPlayerAsync(player1);
        await lotteryService.CreateOtherPlayersAsync(9, 14);
        var players = await lotteryService.GetPlayersAsync();
        await userInteractionService.ShowPlayersTicketInfoAsync(players.Count - 1);
        await lotteryService.DistributePrizesAsync();

        // Assert
        _mockUserInteractionService.Verify(x => x.GetPlayerTicketInputAsync(), Times.Once);
        _mockLotteryService.Verify(x => x.AddPlayerAsync(player1), Times.Once);
        _mockLotteryService.Verify(x => x.CreateOtherPlayersAsync(9, 14), Times.Once);
        _mockLotteryService.Verify(x => x.DistributePrizesAsync(), Times.Once);
        _mockUserInteractionService.Verify(x => x.ShowPlayersTicketInfoAsync(It.IsAny<int>()), Times.Once);
    }
}