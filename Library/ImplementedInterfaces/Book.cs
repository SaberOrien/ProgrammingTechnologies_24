using Data.AbstractInterfaces;

namespace Data.ImplementedInterfaces
{
    internal class Book : IItem
    {
        public Book(int id, string title, int publicationYear, string author, string itemType)
        {
            this.Id = id;
            this.Title = title;
            this.PublicationYear = publicationYear;
            this.Author = author;
            this.ItemType = itemType;

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set;}
        public string Author { get; set; }
        public string ItemType { get; set; }
    }
}
