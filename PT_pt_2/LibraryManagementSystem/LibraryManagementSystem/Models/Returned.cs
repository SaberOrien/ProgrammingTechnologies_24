namespace LibraryData.Models
{
    public class Returned : Event
    {
        public int UserId { get; private set; }
        public string Condition { get; private set; }

        public Returned(int itemId, DateTime eventDate, int userId, string condition = "Good")
            : base(itemId, eventDate)
        {
            UserId = userId;
            Condition = condition;
        }
    }
}

