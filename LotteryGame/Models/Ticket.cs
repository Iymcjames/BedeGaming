
namespace LotteryGame.Models
{
    public class Ticket
    {
        public Player Owner { get; }
        public Ticket(Player owner)
        {
            Owner = owner;
        }
    }
}