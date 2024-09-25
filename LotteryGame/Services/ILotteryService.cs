using LotteryGame.Models;

namespace LotteryGame.Interfaces;

public interface ILotteryService
{
    Task AddPlayerAsync(Player player);
    Task CreateOtherPlayersAsync(int minNumberOfPlayers, int maxNumberOfPlayers);
    Task<float> TotalRevenueAsync();
    Task<List<Player>> GetPlayersAsync();
    Task DistributePrizesAsync();
}
