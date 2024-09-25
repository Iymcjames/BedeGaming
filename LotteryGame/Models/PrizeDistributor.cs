using System.Globalization;
using LotteryGame.Interfaces;

namespace LotteryGame.Models;

public class PrizeDistributor : IPrizeDistributor
{
    private readonly Random _random;

    public PrizeDistributor()
    {
        _random = new Random();
    }

    public async Task DistributeAsync(List<Player> players)
    {
        var tickets = players.SelectMany(p => p.Tickets).ToList();
        int totalTickets = tickets.Count;
        var totalRevenue = totalTickets;

        if (tickets.Count == 0) return;

        Console.WriteLine("Ticket Draw Results:");


        var grandPrizeTicket = tickets[_random.Next(totalTickets)];
        tickets.Remove(grandPrizeTicket);
        var grandPrize = totalRevenue * 0.5m;
        Console.WriteLine($"* Grand Prize: Player {grandPrizeTicket.Owner.Number} wins {grandPrize.ToString("C", CultureInfo.GetCultureInfo("en-US"))}!");

        int secondTierWinners = (int)Math.Round(totalTickets * 0.1);
        var secondTierPrize = totalRevenue * 0.3m / secondTierWinners;
        var secondTierWinnerList = new List<int>();

        for (int i = 0; i < secondTierWinners && tickets.Any(); i++)
        {
            var ticket = tickets[_random.Next(tickets.Count)];
            tickets.Remove(ticket);
            secondTierWinnerList.Add(ticket.Owner.Number);
        }
        if (secondTierWinnerList.Any())
        {
            var joinedSecondWinners = string.Join(", ", secondTierWinnerList);
            Console.WriteLine($"* Second Tier: Players {joinedSecondWinners} win {secondTierPrize.ToString("C", CultureInfo.GetCultureInfo("en-US"))} each!");

        }



        int thirdTierWinners = (int)Math.Round(totalTickets * 0.2);
        var thirdTierPrize = totalRevenue * 0.1m / thirdTierWinners;
        var thirdTierWinnerList = new List<int>();

        for (int i = 0; i < thirdTierWinners && tickets.Any(); i++)
        {
            var ticket = tickets[_random.Next(tickets.Count)];
            tickets.Remove(ticket);
            thirdTierWinnerList.Add(ticket.Owner.Number);

        }
        if (thirdTierWinnerList.Any())
        {
            var joinedThirdWinners = string.Join(", ", thirdTierWinnerList);
            Console.WriteLine($"* Third Tier: Players {joinedThirdWinners} win {thirdTierPrize.ToString("C", CultureInfo.GetCultureInfo("en-US"))} each!");
        }

        Console.WriteLine("Congratulations to the winners!");


        var houseRevenue = totalRevenue - grandPrize - secondTierWinners * secondTierPrize - thirdTierWinners * thirdTierPrize;
        Console.WriteLine($"House Revenue: {houseRevenue.ToString("C", CultureInfo.GetCultureInfo("en-US"))}");

        await Task.CompletedTask;
    }
}
