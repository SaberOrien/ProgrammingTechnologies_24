using LibraryData.Models;

namespace LibraryData.Repositories
{
    public interface IProcessStateRepository
    {
        IEnumerable<Item> GetAvailableItems();
        IEnumerable<Item> GetCheckedOutItems();
    }
}

