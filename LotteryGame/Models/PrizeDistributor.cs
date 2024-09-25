using LotteryGame.Interfaces;
using LotteryGame.Services;

namespace LotteryGame.Models;

public class PrizeDistributor : IPrizeDistributor
{
    private static readonly Random _random = new Random();
    private readonly IUserInteractionService _userInteractionService;

    public PrizeDistributor(IUserInteractionService userInteractionService)
    {
        _userInteractionService = userInteractionService;
    }

    public async Task DistributeAsync(List<Player> players)
    {
        var tickets = players?.SelectMany(p => p.Tickets).ToList();
        int totalTickets = tickets.Count;
        var totalRevenue = totalTickets;

        if (tickets.Count == 0) return;

        var grandPrizeTicket = tickets[_random.Next(totalTickets)];
        tickets.Remove(grandPrizeTicket);
        var grandPrize = totalRevenue * 0.5;

        int secondTierWinners = (int)Math.Round(totalTickets * 0.1);
        var secondTierPrize = totalRevenue * 0.3 / secondTierWinners;
        var secondTierWinnerList = new List<int>();

        for (int i = 0; i < secondTierWinners && tickets.Any(); i++)
        {
            var ticket = tickets[_random.Next(tickets.Count)];
            tickets.Remove(ticket);
            secondTierWinnerList.Add(ticket.Owner.Number);
        }
        var joinedSecondWinners = string.Join(", ", secondTierWinnerList);

        int thirdTierWinners = (int)Math.Round(totalTickets * 0.2);
        var thirdTierPrize = totalRevenue * 0.1 / thirdTierWinners;
        var thirdTierWinnerList = new List<int>();

        for (int i = 0; i < thirdTierWinners && tickets.Any(); i++)
        {
            var ticket = tickets[_random.Next(tickets.Count)];
            tickets.Remove(ticket);
            thirdTierWinnerList.Add(ticket.Owner.Number);

        }

        var joinedThirdWinners = string.Join(", ", thirdTierWinnerList);

        var houseRevenue = totalRevenue - grandPrize - secondTierWinners * secondTierPrize - thirdTierWinners * thirdTierPrize;

        await _userInteractionService.ShowTicketDrawResultAsync(grandPrizeTicket.Owner.Number, grandPrize, joinedSecondWinners, secondTierPrize, joinedThirdWinners, thirdTierPrize, houseRevenue);
        await Task.CompletedTask;
    }
}
