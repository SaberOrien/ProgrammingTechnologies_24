namespace LibraryData.Models
{
    public abstract class Event
    {
        public int ItemId { get; private set; }
        public DateTime EventDate { get; private set; }

        protected Event(int itemId, DateTime eventDate)
        {
            ItemId = itemId;
            EventDate = eventDate;
        }
    }
}

