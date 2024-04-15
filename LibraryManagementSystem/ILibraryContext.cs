using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public interface ILibraryContext
    {
        List<Reader> Readers { get; }
        List<Librarian> Librarians { get; }
        Dictionary<int, Item> Catalog { get; }
        List<Event> Events { get; }

        IEnumerable<Reader> ActiveBorrowers { get; }
        IEnumerable<Item> BorrowedItems { get; }
        public IEnumerable<Item> AvailableItems { get; }


        void AddReader(Reader reader);
        void RemoveReader(int readerId);
        void AddLibrarian(Librarian librarian);
        void RemoveLibrarian(int librarianId);
        void AddItem(Item item);
        void RemoveItem(int itemId);
        void AddEvent(Event newEvent);
        IEnumerable<Item> FindItemsByCriteria(string title = null, string author = null, string publisher = null, int? year = null);
        Reader FindReaderById(int id);
        IEnumerable<Reader> FindReadersBySurname(string surname);
        IEnumerable<Librarian> FindLibrariansBySurname(string surname);
        void UpdateItem(Item updatedItem);
    }
}
