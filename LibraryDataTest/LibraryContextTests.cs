using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LibraryManagementSystem.TestUtilities;

[TestClass]
public class LibraryContextTests
{
    private LibraryContext _libraryContext;
    private RandomDataGenerator _dataGenerator;
    private List<Item> _generatedItems;
    private List<Reader> _generatedReaders;
    private List<Librarian> _generatedLibrarians;
    private List<Event> _generatedEvents;
    private Dictionary<int, Item> _catalog;

    [TestInitialize]
    public void Setup()
    {
        _dataGenerator = new RandomDataGenerator();

        // Generate random data
        _generatedItems = _dataGenerator.GenerateRandomItems(10);
        _generatedReaders = _dataGenerator.GenerateRandomReaders(5);
        _generatedLibrarians = _dataGenerator.GenerateRandomLibrarians(3);
        _generatedEvents = _dataGenerator.GenerateRandomEvents(10, _generatedItems.Select(item => item.Id).ToList());
        _catalog = _generatedItems.ToDictionary(item => item.Id);

        // Create LibraryContext with injected dependencies
        _libraryContext = new LibraryContext(_generatedReaders, _generatedLibrarians, _catalog, _generatedEvents);
    }

    [TestMethod]
    public void AddReader_AddsNewReader()
    {
        DateTime date = DateTime.Now;
        var reader = new Reader { Id = 1, Name = "John", Surname = "Doe", DateOfSignUp = date };
        _libraryContext.AddReader(reader);

        Assert.IsTrue(_libraryContext.Readers.Contains(reader));
    }

    [TestMethod]
    public void RemoveReader_RemovesExistingReader()
    {
        var reader = _generatedReaders.First();
        _libraryContext.RemoveReader(reader.Id);
        Assert.IsFalse(_libraryContext.Readers.Any(r => r.Id == reader.Id));
    }

    [TestMethod]
    public void FindReader_FindsExistingReader()
    {
        // Use an existing randomly generated reader
        var reader = _generatedReaders.First();
        var foundReader = _libraryContext.FindReaderById(reader.Id);

        Assert.IsNotNull(foundReader);
        Assert.AreEqual(reader.Name, foundReader.Name);
    }


    [TestMethod]
    public void AddLibrarian_AddsNewLibrarian()
    {
        var librarian = new Librarian { Id = 1, Name = "Jane", Surname = "Doe" };
        _libraryContext.AddLibrarian(librarian);

        Assert.IsTrue(_libraryContext.Librarians.Contains(librarian));
    }

    [TestMethod]
    public void RemoveLibrarian_RemovesExistingLibrarian()
    {
        var librarian = new Librarian { Id = 1, Name = "Jane", Surname = "Doe" };
        _libraryContext.AddLibrarian(librarian);
        _libraryContext.RemoveLibrarian(1);

        Assert.IsFalse(_libraryContext.Librarians.Any(l => l.Id == 1));
    }

    [TestMethod]
    public void AddItem_AddsNewItem_WhenItemDoesNotExist()
    {
        var item = new Item { Id = 15, Title = "New Book", Author = "Author Name", PublishingYear = 2017, Publisher = "Pub Y", copiesInStock = 10, copiesBorrowed = 0 };

        _libraryContext.AddItem(item);

        Assert.IsTrue(_libraryContext.Catalog.ContainsKey(item.Id));
        Assert.AreEqual("New Book", _libraryContext.Catalog[item.Id].Title);
    }

    [TestMethod]
    public void RemoveItem_RemovesItem_WhenItemExists()
    {
        // Use an existing randomly generated item
        var item = _generatedItems.First();
        _libraryContext.RemoveItem(item.Id);
        Assert.IsFalse(_libraryContext.Catalog.ContainsKey(item.Id));
    }

    [TestMethod]
    public void AddItem_DoesNotAddItem_WhenItemAlreadyExists()
    {
        // Ensure there is already an item with ID = 1 in the catalog
        Assert.IsTrue(_libraryContext.Catalog.ContainsKey(1), "Item with ID 1 should already exist.");
        var originalItem = _libraryContext.Catalog[1];
        string originalTitle = originalItem.Title;

        // Attempt to add another item with the same ID
        var duplicateItem = new Item { Id = 1, Title = "Duplicate New Book", Author = "Duplicate Author", PublishingYear = 2020, Publisher = "Dup Pub", copiesInStock = 5, copiesBorrowed = 0 };

        try
        {
            _libraryContext.AddItem(duplicateItem);
            Assert.Fail("No exception thrown for adding duplicate item, but one was expected.");
        }
        catch (ArgumentException ex)
        {
            Assert.AreEqual("An item with the same ID already exists.", ex.Message);
        }

        // Verify that the original item was not changed
        Assert.AreEqual(originalTitle, _libraryContext.Catalog[1].Title, "The original item's title should not have changed.");
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddItem_ThrowsException_WhenItemIsNull()
    {

        _libraryContext.AddItem(null); 
    }

    [TestMethod]
    public void AddEvent_AddsNewEvent()
    {
        var newEvent = new Event { UserId = 1, ItemId = 101, Timestamp = DateTime.Now, Type = EventType.TakenOut };
        _libraryContext.AddEvent(newEvent);

        Assert.IsTrue(_libraryContext.Events.Contains(newEvent));
    }

    [TestMethod]
    public void UpdateItem_UpdatesExistingItem()
    {
        // Use an existing randomly generated item and update it
        var item = _generatedItems.First();
        var updatedTitle = "Updated Title";
        item.Title = updatedTitle;
        _libraryContext.UpdateItem(item);

        Assert.AreEqual(updatedTitle, _libraryContext.Catalog[item.Id].Title);
    }
}
