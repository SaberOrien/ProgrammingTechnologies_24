using MVVM.Model.Abstract;

namespace MVVM.Model.Implemented
{
    internal class ItemModel : IItemModel
    {
        public ItemModel(int id, string title, int publicationYear, string author, string itemType)
        {
            Id = id;
            Title = title;
            PublicationYear = publicationYear;
            Author = author;
            ItemType = itemType;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Author { get; set; }
        public string ItemType { get; set; }
    }
}
