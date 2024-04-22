using LibraryData.Models;
namespace LibraryLogic
{
    public interface ILibraryService
    {
        bool RegisterUser(User user);
        bool RemoveUser(int userId);
        bool UpdateUser(User user);

        bool CheckoutItem(int userId, int itemId);
        bool ReturnItem(int userId, int itemId, string condition);

        IEnumerable<Item> GetAllAvailableItems();
        IEnumerable<Item> GetAllCheckedOutItems();

        IEnumerable<Item> GetUserCheckedOutItems(int userId);
        IEnumerable<Item> GetUnreturnedItems(int userId);
    }
}
