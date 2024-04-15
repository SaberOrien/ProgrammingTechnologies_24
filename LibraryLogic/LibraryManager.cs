using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Logic
{
    public class LibraryManager
    {
        private readonly ILibraryContext _context;

        public LibraryManager(ILibraryContext context)
        {
            _context = context;
        }

        public void AddItem(Item item)
        {
            if (!_context.Catalog.ContainsKey(item.Id))
                _context.Catalog.Add(item.Id, item);
        }

        public void RemoveItem(int itemId)
        {
            if (_context.Catalog.ContainsKey(itemId))
                _context.Catalog.Remove(itemId);
        }

        public void RegisterReader(Reader reader)
        {
            _context.Readers.Add(reader);
        }

        public void RemoveReader(int readerId)
        {
            _context.Readers.RemoveAll(r => r.Id == readerId);
        }

        public void RegisterLibrarian(Librarian librarian)
        {
            _context.Librarians.Add(librarian);
        }

        public void RemoveLibrarian(int librarianId)
        {
            _context.Librarians.RemoveAll(l => l.Id == librarianId);
        }

        public bool CheckOutItem(int itemId, int readerId)
        {
            var item = _context.Catalog.GetValueOrDefault(itemId);
            var reader = _context.Readers.Find(r => r.Id == readerId);

            if (item != null && reader != null && item.copiesInStock > item.copiesBorrowed)
            {
                item.copiesBorrowed++;
                var borrowRecord = new BorrowRecord
                {
                    BookId = itemId,
                    DateBorrowed = DateTime.Now
                };
                reader.BooksBorrowed.Add(borrowRecord);
                AddEvent(new Event { UserId = readerId, ItemId = itemId, Timestamp = DateTime.Now, Type = EventType.TakenOut });
                return true;
            }
            return false;
        }

        public bool ReturnItem(int itemId, int readerId)
        {
            var item = _context.Catalog.GetValueOrDefault(itemId);
            var reader = _context.Readers.Find(r => r.Id == readerId);
            var record = reader?.BooksBorrowed.FirstOrDefault(b => b.BookId == itemId && !b.DateReturned.HasValue);

            if (item != null && record != null)
            {
                item.copiesBorrowed--;
                record.DateReturned = DateTime.Now;
                AddEvent(new Event { UserId = readerId, ItemId = itemId, Timestamp = DateTime.Now, Type = EventType.Returned });

                if ((DateTime.Now - record.DateBorrowed).TotalDays > 30)
                {
                    record.PenaltyAmount = CalculatePenalty((DateTime.Now - record.DateBorrowed).TotalDays);
                    record.IsPenaltyPaid = false;
                }
                else
                {
                    record.IsPenaltyPaid = true;
                }

                return true;
            }
            return false;
        }


        private decimal CalculatePenalty(double overdueDays)
        {
            const decimal dailyPenaltyRate = 1.00m; 
            return (decimal)overdueDays * dailyPenaltyRate;
        }

        public void MarkPenaltyAsPaid(int readerId, int itemId)
        {
            var reader = _context.Readers.Find(r => r.Id == readerId);
            var borrowRecord = reader?.BooksBorrowed.FirstOrDefault(b => b.BookId == itemId);

            if (borrowRecord != null && borrowRecord.PenaltyAmount > 0)
            {
                borrowRecord.IsPenaltyPaid = true;
            }
        }

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
        }

        public IEnumerable<Reader> FindReadersWithUnpaidPenalties()
        {
            return _context.Readers.Where(r => r.BooksBorrowed.Any(b => b.PenaltyAmount > 0 && !b.IsPenaltyPaid));
        }
        public void ReportCurrentState()
        {
            var availableItems = _context.AvailableItems;
            var borrowedItems = _context.BorrowedItems;
            var activeBorrowers = _context.ActiveBorrowers;
        }
    }

}
