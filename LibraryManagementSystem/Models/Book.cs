namespace LibraryData.Models
{
    public class Book : Item
    {
        public string Author { get; set; }
        public string ISBN { get; set; }

        public Book(int id, string title, string publisher, bool isAvailable, string author, string isbn)
            : base(id, title, publisher, isAvailable)
        {
            Author = author;
            ISBN = isbn;
        }
    }
}
