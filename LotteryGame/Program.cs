using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
                .AddSingleton<IUserInteractionService, ConsoleUserInteractionService>()
                .AddSingleton<IPrizeDistributor, PrizeDistributor>()
                .AddSingleton<ILotteryService, LotteryService>()
                .BuildServiceProvider();

var lotteryService = serviceProvider.GetService<ILotteryService>();
var userInteractionService = serviceProvider.GetService<IUserInteractionService>();

await userInteractionService.ShowGameEntryInformationAsync();

var player1 = new Player(1, 10);
int player1Tickets = await userInteractionService.GetPlayerTicketInputAsync();

player1.BuyTickets(player1Tickets);
await lotteryService.AddPlayerAsync(player1);

await lotteryService.CreateOtherPlayersAsync(9, 14);
var players = await lotteryService.GetPlayersAsync();
await userInteractionService.ShowPlayersTicketInfoAsync(players.Count - 1);

await lotteryService.DistributePrizesAsync();
