namespace LibraryData.Models
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public bool IsAvailable { get; set; }

        protected Item(int id, string title, string publisher, bool isAvailable)
        {
            Id = id;
            Title = title;
            Publisher = publisher;
            IsAvailable = isAvailable;
        }
    }
}
