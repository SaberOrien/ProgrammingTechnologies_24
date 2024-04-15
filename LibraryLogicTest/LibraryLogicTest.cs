using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Logic;
using LibraryManagementSystem.TestUtilities;
using System.Linq;

[TestClass]
public class LibraryLogicTest
{
    private LibraryManager _manager;
    private LibraryContext _context;
    private ScenarioDataGenerator _dataGenerator;

    [TestInitialize]
    public void Setup()
    {
        _context = new LibraryContext(); // Assuming LibraryContext implements ILibraryContext
        _manager = new LibraryManager(_context);
        _dataGenerator = new ScenarioDataGenerator();

        var (readers, items, events) = _dataGenerator.GenerateOverdueItemsScenario();
        foreach (var reader in readers)
            _context.Readers.Add(reader);
        foreach (var item in items)
            _context.Catalog.Add(item.Id, item);
        foreach (var eventItem in events)
            _context.Events.Add(eventItem);
    }

    [TestMethod]
    public void CheckOutItem_ShouldReturnFalse_WhenNoCopiesAvailable()
    {
        var result = _manager.CheckOutItem(101, 20); 
        Assert.IsFalse(result, "Expected to fail check out due to no available copies.");
    }

    [TestMethod]
    public void CheckOutItem_ShouldSuccessfullyCheckOutBook()
    {
        bool result = _manager.CheckOutItem(102, 20);
        Assert.IsTrue(result, "Book should be successfully checked out");
        Assert.AreEqual(1, _context.Catalog[102].copiesBorrowed, "Copies borrowed should be incremented");
    }

    [TestMethod]
    public void ReturnItem_ShouldReturnBookOnTime_WithoutPenalty()
    {
        _manager.CheckOutItem(102, 21);  // Checkout first to return later
        System.Threading.Thread.Sleep(1000); // Simulate some time delay
        bool result = _manager.ReturnItem(102, 21);
        Assert.IsTrue(result, "Book should be returned successfully");
        var borrowRecord = _context.Readers.First(r => r.Id == 21).BooksBorrowed.First();
        Assert.AreEqual(0m, borrowRecord.PenaltyAmount, "No penalty should be charged for on-time return");
    }

    [TestMethod]
    public void ReturnItem_ShouldReturnTrue_AndHandlePenalties_WhenItemIsOverdue()
    {
        var reader = _context.Readers.First(r => r.Id == 20);
        var item = _context.Catalog[101];
        var borrowRecord = new BorrowRecord
        {
            BookId = item.Id,
            DateBorrowed = DateTime.Now.AddDays(-40) // Borrowed 40 days ago
        };
        reader.BooksBorrowed.Add(borrowRecord);

        var result = _manager.ReturnItem(101, 20);

        var updatedRecord = reader.BooksBorrowed.FirstOrDefault(b => b.BookId == 101);

        Assert.IsTrue(result, "Expected the return to succeed.");
        Assert.IsNotNull(updatedRecord?.DateReturned, "Expected the return date to be set.");
        Assert.IsTrue(updatedRecord.PenaltyAmount > 0, "Expected a penalty for being overdue.");
    }

    [TestMethod]
    public void PayPenalty_ShouldMarkPenaltyAsPaid()
    {
        _manager.CheckOutItem(102, 20);
        var reader = _context.Readers.First(r => r.Id == 20);
        reader.BooksBorrowed.Add(new BorrowRecord { BookId = 102, DateBorrowed = DateTime.Now.AddDays(-40), DateReturned = DateTime.Now, PenaltyAmount = 50, IsPenaltyPaid = false });

        _manager.MarkPenaltyAsPaid(20, 102);
        Assert.IsTrue(reader.BooksBorrowed.First().IsPenaltyPaid, "Penalty should be marked as paid");
    }

    [TestMethod]
    public void FindReadersWithUnpaidPenalties_ShouldReturnReadersWithOutstandingPenalties()
    {
        _manager.ReturnItem(101, 20);
        _manager.ReturnItem(102, 21);

        var unpaidPenalties = _manager.FindReadersWithUnpaidPenalties();

        Assert.IsTrue(unpaidPenalties.Any(), "Expected at least one reader with unpaid penalties.");
        Assert.IsTrue(unpaidPenalties.Any(r => r.Id == 20), "Expected reader ID 20 to have unpaid penalties.");
    }

    [TestMethod]
    public void GetHistoryOfActivityForUser()
    {
        _context.Events.AddRange(new Event[] {
            new Event { UserId = 1, ItemId = 101, Timestamp = DateTime.Now.AddDays(-10), Type = EventType.TakenOut },
            new Event { UserId = 1, ItemId = 101, Timestamp = DateTime.Now.AddDays(-5), Type = EventType.Returned }
        });

        var activity = _context.Events.Where(e => e.UserId == 1).ToList();
        Assert.AreEqual(2, activity.Count, "Should retrieve all activity for a user");
    }

    [TestMethod]
    public void GetRecordOfUnreturnedBooksForUser()
    {
        var unreturnedBooksCurCount = _context.Readers.First(r => r.Id == 21).BooksBorrowed.Where(b => !b.DateReturned.HasValue).ToList().Count();
        _manager.CheckOutItem(102, 21);  
        unreturnedBooksCurCount++;
        var unreturnedBooks = _context.Readers.First(r => r.Id == 21).BooksBorrowed.Where(b => !b.DateReturned.HasValue).ToList();
        Assert.AreEqual(unreturnedBooksCurCount, unreturnedBooks.Count, "Should retrieve unreturned books for a user");
    }
}
