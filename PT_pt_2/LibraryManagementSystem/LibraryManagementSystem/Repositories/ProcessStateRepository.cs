using LibraryData.Models;

namespace LibraryData.Repositories
{
    public class ProcessStateRepository : IProcessStateRepository
    {
        private readonly List<Item> _items;

        public ProcessStateRepository(List<Item> items)
        {
            _items = items;
        }

        public IEnumerable<Item> GetAvailableItems()
        {
            return _items.Where(item => item.IsAvailable).ToList();
        }

        public IEnumerable<Item> GetCheckedOutItems()
        {
            return _items.Where(item => !item.IsAvailable).ToList();
        }
    }
}
