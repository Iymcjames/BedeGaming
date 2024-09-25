using LotteryGame.Models;

namespace LotteryGame.Interfaces;

public interface IPrizeDistributor
{
    Task DistributeAsync(List<Player> players);
}
