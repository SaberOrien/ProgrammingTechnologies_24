using LibraryManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace LibraryManagementSystem.Data
{
    public class LibraryContext : ILibraryContext
    {
        public List<Reader> Readers { get; private set; }
        public List<Librarian> Librarians { get; private set; }
        public Dictionary<int, Item> Catalog { get; private set; }
        public List<Event> Events { get; private set; }
        public IEnumerable<Item> AvailableItems => Catalog.Values.Where(i => i.copiesInStock > i.copiesBorrowed);
        public IEnumerable<Item> BorrowedItems => Catalog.Values.Where(i => i.copiesBorrowed > 0);
        public IEnumerable<Reader> ActiveBorrowers => Readers.Where(r => r.BooksBorrowed.Any(b => !b.DateReturned.HasValue));

        public LibraryContext(List<Reader> readers, List<Librarian> librarians, Dictionary<int, Item> catalog, List<Event> events)
        {
            Readers = readers ?? throw new ArgumentNullException(nameof(readers));
            Librarians = librarians ?? throw new ArgumentNullException(nameof(librarians));
            Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            Events = events ?? throw new ArgumentNullException(nameof(events));
        }


        public void AddReader(Reader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "Reader cannot be null.");
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Surname))
                throw new ArgumentException("Reader must have both name and surname.");
            Readers.Add(reader);
        }

        public void RemoveReader(int readerId)
        {
            Readers.RemoveAll(r => r.Id == readerId);
        }

        public void AddLibrarian(Librarian librarian)
        {
            if(librarian == null)
                throw new ArgumentNullException(nameof(librarian), "Librarian cannot be null.");
            if (string.IsNullOrWhiteSpace(librarian.Name) || string.IsNullOrWhiteSpace(librarian.Surname))
                throw new ArgumentException("Librarian must have both name and surname.");
            Librarians.Add(librarian);
        }

        public void RemoveLibrarian(int librarianId)
        {
            Librarians.RemoveAll(l => l.Id == librarianId);
        }

        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            if (string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Author))
                throw new ArgumentException("Item must have a title and author.");
            if (string.IsNullOrWhiteSpace(item.Publisher))
                throw new ArgumentException("Item must have a publisher.");
            if (item.PublishingYear <= 0 || item.PublishingYear > DateTime.Now.Year)
                throw new ArgumentException("Item must have a valid publishing year.");
            if (item.copiesInStock < 0)
                throw new ArgumentException("Copies in stock cannot be negative.");
            if (item.copiesBorrowed < 0 || item.copiesBorrowed > item.copiesInStock)
                throw new ArgumentException("Copies borrowed must be non-negative and cannot exceed copies in stock.");

            if (!Catalog.ContainsKey(item.Id))
                Catalog.Add(item.Id, item);
            else
                throw new ArgumentException("An item with the same ID already exists.");
        }

        public void RemoveItem(int itemId)
        {
            Catalog.Remove(itemId);
        }

        public void AddEvent(Event newEvent)
    {
            Events.Add(newEvent);
        }

        public IEnumerable<Item> FindItemsByCriteria(string title = null, string author = null, string publisher = null, int? year = null)
        {
            return Catalog.Values.Where(i =>
                (title == null || i.Title.Contains(title)) &&
                (author == null || i.Author.Contains(author)) &&
                (publisher == null || i.Publisher.Contains(publisher)) &&
                (!year.HasValue || i.PublishingYear == year.Value));
        }

        public Reader FindReaderById(int id)
        {
            return Readers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reader> FindReadersBySurname(string surname)
        {
            return Readers.Where(r => r.Surname.Contains(surname));
        }

        public IEnumerable<Librarian> FindLibrariansBySurname(string surname)
        {
            return Librarians.Where(l => l.Surname.Contains(surname));
        }

        public void UpdateItem(Item updatedItem)
        {
            if (updatedItem == null)
                throw new ArgumentNullException(nameof(updatedItem), "Updated item cannot be null.");
            if (string.IsNullOrWhiteSpace(updatedItem.Title) || string.IsNullOrWhiteSpace(updatedItem.Author))
                throw new ArgumentException("Updated item must have a title and author.");
            if (string.IsNullOrWhiteSpace(updatedItem.Publisher))
                throw new ArgumentException("Updated item must have a publisher.");
            if (updatedItem.PublishingYear <= 0 || updatedItem.PublishingYear > DateTime.Now.Year)
                throw new ArgumentException("Updated item must have a valid publishing year.");
            if (updatedItem.copiesInStock < 0)
                throw new ArgumentException("Copies in stock cannot be negative.");
            if (updatedItem.copiesBorrowed < 0 || updatedItem.copiesBorrowed > updatedItem.copiesInStock)
                throw new ArgumentException("Copies borrowed must be non-negative and cannot exceed copies in stock.");

            if (Catalog.ContainsKey(updatedItem.Id))
            {
                var existingItem = Catalog[updatedItem.Id];
                existingItem.Title = updatedItem.Title;
                existingItem.Author = updatedItem.Author;
                existingItem.Publisher = updatedItem.Publisher;
                existingItem.PublishingYear = updatedItem.PublishingYear;
                existingItem.copiesInStock = updatedItem.copiesInStock;
                existingItem.copiesBorrowed = updatedItem.copiesBorrowed;
            }
            else
            {
                throw new KeyNotFoundException($"No item found with ID {updatedItem.Id} to update.");
            }
        }

        public LibraryState GetLibraryState()
        {
            return new LibraryState
            {
                TotalItems = Catalog.Count,
                AvailableItems = AvailableItems.Count(),
                BorrowedItems = BorrowedItems.Count(),
                ActiveBorrowers = ActiveBorrowers.Count(),
                OverdueItems = BorrowedItems.Count(i => (DateTime.Now - Events.Last(e => e.ItemId == i.Id && e.Type == EventType.TakenOut).Timestamp).TotalDays > 30)
            };
        }
    }
}


