using LibraryData.Models;

namespace LibraryData.Repositories
{
    public interface ICatalogRepository
    {
        Item GetItem(int itemId);
        IEnumerable<Item> GetAllItems();
        void AddItem(Item item);
        void UpdateItem(Item item);
    }
}

