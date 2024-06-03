namespace Data.AbstractInterfaces
{
    public interface IItem
    {
        int Id { get; set; }
        string Title { get; set; }
        int PublicationYear { get; set; }
        string Author { get; set; }
        string ItemType { get; set; }
    }
}
