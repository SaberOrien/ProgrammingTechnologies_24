namespace LibraryData.Models
{
    public class TakenOut : Event
    {
        public int UserId { get; private set; }

        public TakenOut(int itemId, DateTime eventDate, int userId)
            : base(itemId, eventDate)
        {
            UserId = userId;
        }
    }
}

