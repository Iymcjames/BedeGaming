
using System.Globalization;

namespace LotteryGame.Services;

public class ConsoleUserInteractionService : IUserInteractionService
{

    public async Task ShowGameEntryInformationAsync()
    {
        Console.WriteLine("welcome to Bede Lottery, Player 1!");
        Console.WriteLine("* Your digital balance: $10.00");
        Console.WriteLine("* Ticket Price: $1.00 each");

        await Task.CompletedTask;
    }

    public async Task ShowPlayersTicketInfoAsync(int playerCount)
    {
        Console.WriteLine($"{playerCount} other CPU players have also purchased tickets");
        await Task.CompletedTask;
    }

    public async Task ShowTicketDrawResultAsync(int grandPriceNumber, double grandPrice, string secondTierWinners, double sedondTierPrice, string thirdTierWinners, double thirdTierPrice, double houseRevenue)
    {
        Console.WriteLine("Ticket Draw Results:");
        Console.WriteLine($"* Grand Prize: Player {grandPriceNumber} wins {grandPrice.ToString("C", CultureInfo.GetCultureInfo("en-US"))}!");
        Console.WriteLine($"* Second Tier: Players {secondTierWinners} win {sedondTierPrice.ToString("C", CultureInfo.GetCultureInfo("en-US"))} each!");
        Console.WriteLine($"* Third Tier: Players {thirdTierWinners} win {thirdTierPrice.ToString("C", CultureInfo.GetCultureInfo("en-US"))} each!");
        Console.WriteLine("Congratulations to the winners!");
        Console.WriteLine($"House Revenue: {houseRevenue.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");

        await Task.CompletedTask;
    }

    public async Task<int> GetPlayerTicketInputAsync()
    {
        int player1Tickets;
        do
        {
            Console.WriteLine("How many tickets do you want to buy, Player 1?");
            player1Tickets = int.Parse(Console.ReadLine());
            if (player1Tickets < 1 || player1Tickets > 10)
            {
                Console.WriteLine("The number of tickets must be between 1 and 10.");
            }
        } while (player1Tickets < 1 || player1Tickets > 10);

        return await Task.FromResult(player1Tickets);

    }
}
