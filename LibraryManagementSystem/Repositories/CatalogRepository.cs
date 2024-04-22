using LibraryData.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryData.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<Item> _items = new List<Item>();

        public Item GetItem(int itemId)
        {
            return _items.FirstOrDefault(i => i.Id == itemId);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _items;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var existingItem = GetItem(item.Id);
            if (existingItem != null)
            {
                existingItem.Title = item.Title;
                existingItem.Publisher = item.Publisher;
                existingItem.IsAvailable = item.IsAvailable;

                if (item is Book book && existingItem is Book existingBook)
                {
                    existingBook.Author = book.Author;
                    existingBook.ISBN = book.ISBN;
                }
                else if (item is Magazine magazine && existingItem is Magazine existingMagazine)
                {
                    existingMagazine.IssueNumber = magazine.IssueNumber;
                    existingMagazine.Month = magazine.Month;
                }
            }
        }
    }
}
