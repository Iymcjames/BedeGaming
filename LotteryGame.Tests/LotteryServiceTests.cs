using Moq;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using LotteryGame.Models;

namespace LotteryGame.Tests;
public class LotteryServiceTests
{
    [Fact]
    public async Task AddPlayerAsync_Should_Add_Player_To_List()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        var service = new LotteryService(mockPrizeDistributor.Object);
        var player = new Player(1, 10);

        await service.AddPlayerAsync(player);

        var players = await service.GetPlayersAsync();
        Assert.Contains(player, players);
    }

    [Fact]
    public async Task CreateOtherPlayersAsync_Should_Add_Correct_Number_Of_Players()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        var service = new LotteryService(mockPrizeDistributor.Object);

        await service.CreateOtherPlayersAsync(9, 14);

        var players = await service.GetPlayersAsync();
        Assert.InRange(players.Count, 10, 15);
    }


    [Fact]
    public async Task TotalRevenueAsync_Should_Return_Correct_Revenue()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        var service = new LotteryService(mockPrizeDistributor.Object);
        var player = new Player(2, 10);

        player.BuyTickets(3);
        await service.AddPlayerAsync(player);

        var totalRevenue = await service.TotalRevenueAsync();
        Assert.Equal(3m, totalRevenue);
    }

    [Fact]
    public async Task DistributePrizesAsync_Should_Call_PrizeDistributor()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        mockPrizeDistributor.Setup(pd => pd.DistributeAsync(It.IsAny<List<Player>>()))
                            .Returns(Task.CompletedTask);

        var service = new LotteryService(mockPrizeDistributor.Object);
        var player = new Player(3, 10);

        player.BuyTickets(5);
        await service.AddPlayerAsync(player);
        await service.DistributePrizesAsync();

        mockPrizeDistributor.Verify(pd => pd.DistributeAsync(It.IsAny<List<Player>>()), Times.Once);
    }

    [Fact]
    public async Task Player_Cannot_Buy_More_Tickets_Than_Balance()
    {
        var player = new Player(4, 10);
        player.BuyTickets(15);

        Assert.Equal(10, player.Tickets.Count);
    }

    [Fact]
    public async Task RandomizeCpuPlayersAsync_Should_Limit_Tickets_To_Balance()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        var service = new LotteryService(mockPrizeDistributor.Object);

        await service.CreateOtherPlayersAsync(9, 14);

        var players = await service.GetPlayersAsync();
        foreach (var player in players.Skip(1))
        {
            Assert.InRange(player.Tickets.Count, 1, 10);
        }
    }

    [Fact]
    public async Task PrizeDistribution_Should_Allocate_Prizes_Correctly()
    {
        var mockPrizeDistributor = new Mock<IPrizeDistributor>();
        var service = new LotteryService(mockPrizeDistributor.Object);
        var player1 = new Player(1, 10);
        var player2 = new Player(2, 10);

        player1.BuyTickets(5);
        player2.BuyTickets(5);

        await service.AddPlayerAsync(player1);
        await service.AddPlayerAsync(player2);

        await service.DistributePrizesAsync();

        mockPrizeDistributor.Verify(pd => pd.DistributeAsync(It.IsAny<List<Player>>()), Times.Once);
    }
}


