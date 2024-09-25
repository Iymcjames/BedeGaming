using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IPrizeDistributor, PrizeDistributor>()
    .AddSingleton<ILotteryService, LotteryService>()
    .BuildServiceProvider();

var lotteryService = serviceProvider.GetService<ILotteryService>();
Console.WriteLine("welcome to Bede Lottery, Player 1!");
Console.WriteLine("* Your digital balance: $10.00");
Console.WriteLine("* Ticket Price: $1.00 each");

var player1 = new Player(1, 10);
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

player1.BuyTickets(player1Tickets);
await lotteryService.AddPlayerAsync(player1);

await lotteryService.CreateOtherPlayersAsync(9, 14);
var players = await lotteryService.GetPlayersAsync();

Console.WriteLine($"{players.Count - 1} other CPU players have also purchased tickets");

await lotteryService.DistributePrizesAsync();
