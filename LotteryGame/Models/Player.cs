namespace LotteryGame.Models
{
    public class Player
    {
        public int Number { get; }
        public int Balance { get; private set; }
        public List<Ticket> Tickets { get; private set; }

        public Player(int number, int balance)
        {
            Number = number;
            Balance = balance;
            Tickets = new List<Ticket>();
        }

        public void BuyTickets(int ticketCount)
        {
            int maximumAffordableTickets = Math.Min(ticketCount, Balance);
            for (int i = 0; i < maximumAffordableTickets; i++)
            {
                Tickets.Add(new Ticket(this));
            }
            Balance -= maximumAffordableTickets;
        }
    }
}