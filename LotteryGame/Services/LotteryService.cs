using LotteryGame.Interfaces;
using LotteryGame.Models;

namespace LotteryGame.Services;

public class LotteryService : ILotteryService
{
    private readonly List<Player> _players;
    private readonly IPrizeDistributor _prizeDistributor;
    private readonly Random _random;

    public LotteryService(IPrizeDistributor prizeDistributor)
    {
        _players = new List<Player>();
        _random = new Random();
        _prizeDistributor = prizeDistributor;
    }

    public async Task AddPlayerAsync(Player player)
    {
        _players.Add(player);
        await Task.CompletedTask;
    }

    public async Task CreateOtherPlayersAsync(int minNumberOfPlayers, int maxNumberOfPlayers)
    {
        int otherPlayers = _random.Next(minNumberOfPlayers, maxNumberOfPlayers + 1);
        for (int i = 2; i <= otherPlayers; i++)
        {
            var otherPlayer = new Player(i, 10);
            int ticketCount = _random.Next(1, 11);
            otherPlayer.BuyTickets(ticketCount);
            await AddPlayerAsync(otherPlayer);
        }
    }

    public async Task<float> TotalRevenueAsync()
    {
        int totalTickets = _players.Sum(p => p.Tickets.Count);
        return await Task.FromResult(totalTickets);
    }

    public async Task<List<Player>> GetPlayersAsync()
    {
        return await Task.FromResult(_players);
    }

    public async Task DistributePrizesAsync()
    {
        await _prizeDistributor.DistributeAsync(_players);
    }
}
