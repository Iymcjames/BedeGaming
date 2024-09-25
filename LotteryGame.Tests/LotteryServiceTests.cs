using Moq;
using LotteryGame.Interfaces;
using LotteryGame.Services;
using LotteryGame.Models;

namespace LotteryGame.Tests;
public class LotteryServiceTests
{
    private readonly Mock<IPrizeDistributor> _mockPrizeDistributor;
    private readonly LotteryService _lotteryService;

    public LotteryServiceTests()
    {
        _mockPrizeDistributor = new Mock<IPrizeDistributor>();
        _lotteryService = new LotteryService(_mockPrizeDistributor.Object);
    }

    [Fact]
    public async Task AddPlayerAsync_Should_Add_Player_To_List()
    {
        var player = new Player(1, 10);
        await _lotteryService.AddPlayerAsync(player);
        var players = await _lotteryService.GetPlayersAsync();
        Assert.Single(players);
        Assert.Contains(player, players);
    }

    [Fact]
    public async Task TotalRevenueAsync_Should_Return_Total_Tickets_Sold()
    {
        var player1 = new Player(1, 10);
        player1.BuyTickets(5);
        await _lotteryService.AddPlayerAsync(player1);
        var player2 = new Player(2, 10);
        player2.BuyTickets(3);
        await _lotteryService.AddPlayerAsync(player2);
        var totalRevenue = await _lotteryService.TotalRevenueAsync();
        Assert.Equal(8, totalRevenue);
    }

    [Fact]
    public async Task DistributePrizesAsync_Should_Call_Distribute_On_PrizeDistributor()
    {
        var player1 = new Player(1, 10);
        await _lotteryService.AddPlayerAsync(player1);
        await _lotteryService.DistributePrizesAsync();
        _mockPrizeDistributor.Verify(pd => pd.DistributeAsync(It.IsAny<List<Player>>()), Times.Once);
    }
}


