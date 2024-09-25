using LotteryGame.Services;

namespace LotteryGame.Tests;

public class ConsoleUserInteractionServiceTests
{
    private readonly ConsoleUserInteractionService _userInteractionService;

    public ConsoleUserInteractionServiceTests()
    {
        _userInteractionService = new ConsoleUserInteractionService();
    }

    [Fact]
    public async Task ShowGameEntryInformationAsync_Should_Print_Welcome_Message()
    {

        using var sw = new StringWriter();
        Console.SetOut(sw);


        await _userInteractionService.ShowGameEntryInformationAsync();


        var output = sw.ToString().Trim();
        Assert.Contains("welcome to Bede Lottery, Player 1!", output);
        Assert.Contains("Your digital balance: $10.00", output);
        Assert.Contains("Ticket Price: $1.00 each", output);
    }

    [Fact]
    public async Task ShowPlayersTicketInfoAsync_Should_Print_Player_Count()
    {

        using var sw = new StringWriter();
        Console.SetOut(sw);
        int playerCount = 5;


        await _userInteractionService.ShowPlayersTicketInfoAsync(playerCount);


        var output = sw.ToString().Trim();
        Assert.Contains("5 other CPU players have also purchased tickets", output);
    }

    [Fact]
    public async Task ShowTicketDrawResultAsync_Should_Print_Draw_Results()
    {

        using var sw = new StringWriter();
        Console.SetOut(sw);
        int grandPrizeNumber = 1;
        double grandPrize = 50.0;
        string secondTierWinners = "2, 3";
        double secondTierPrice = 15.0;
        string thirdTierWinners = "4, 5";
        double thirdTierPrice = 5.0;
        double houseRevenue = 100.0;


        await _userInteractionService.ShowTicketDrawResultAsync(
            grandPrizeNumber,
            grandPrize,
            secondTierWinners,
            secondTierPrice,
            thirdTierWinners,
            thirdTierPrice,
            houseRevenue
        );


        var output = sw.ToString().Trim();
        Assert.Contains("Ticket Draw Results:", output);
        Assert.Contains("* Grand Prize: Player 1 wins $50.00!", output);
        Assert.Contains("* Second Tier: Players 2, 3 win $15.00 each!", output);
        Assert.Contains("* Third Tier: Players 4, 5 win $5.00 each!", output);
        Assert.Contains("House Revenue: $100.00", output);
    }

    [Fact]
    public async Task GetPlayerTicketInputAsync_Should_Return_Valid_Ticket_Count()
    {

        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Mock console input
        var inputs = new StringReader("3\n"); // Simulate the user entering '3'
        Console.SetIn(inputs);


        int result = await _userInteractionService.GetPlayerTicketInputAsync();


        Assert.Equal(3, result);
    }

    [Fact]
    public async Task GetPlayerTicketInputAsync_Should_Prompt_Again_On_Invalid_Input()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);

        var inputs = new StringReader("12\n5\n");
        Console.SetIn(inputs);

        int result = await _userInteractionService.GetPlayerTicketInputAsync();

        Assert.Equal(5, result);
        var output = sw.ToString().Trim();
        Assert.Contains("The number of tickets must be between 1 and 10.", output);
    }
}