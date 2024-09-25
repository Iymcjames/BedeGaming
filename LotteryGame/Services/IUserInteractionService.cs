
namespace LotteryGame.Services;

public interface IUserInteractionService
{
    Task ShowGameEntryInformationAsync();
    Task<int> GetPlayerTicketInputAsync();
    Task ShowPlayersTicketInfoAsync(int playerCount);
    Task ShowTicketDrawResultAsync(int grandPriceNumber, double grandPrice, string secondTierWinners, double sedondTierPrice, string thirdTierWinners, double thirdTierPrice, double houseRevenue);
}
