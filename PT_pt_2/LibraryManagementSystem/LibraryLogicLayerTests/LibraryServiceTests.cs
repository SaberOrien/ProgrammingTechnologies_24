using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LibraryData.Repositories;
using LibraryData.Models;
using LibraryLogic;
using System;

namespace LibraryLogicLayerTests
{
    [TestClass]
    public class LibraryServiceTests
    {
        private Mock<ICatalogRepository> _catalogRepoMock;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IEventRepository> _eventRepoMock;
        private Mock<IProcessStateRepository> _processStateRepoMock;
        private LibraryService _libraryService;

        [TestInitialize]
        public void Setup()
        {
            _catalogRepoMock = new Mock<ICatalogRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _eventRepoMock = new Mock<IEventRepository>();
            _processStateRepoMock = new Mock<IProcessStateRepository>();

            _libraryService = new LibraryService(_catalogRepoMock.Object, _userRepoMock.Object,
                                                 _eventRepoMock.Object, _processStateRepoMock.Object);
        }

        [TestMethod]
        public void CheckoutItem_ItemIsAvailable_ReturnsTrue()
        {
            var itemId = 1;
            var userId = 1;
            var item = new Book(itemId, "Sample Book", "Publisher", true, "Author", "ISBN123");

            _catalogRepoMock.Setup(repo => repo.GetItem(itemId)).Returns(item);
            _eventRepoMock.Setup(repo => repo.AddEvent(It.IsAny<Event>()));

            var result = _libraryService.CheckoutItem(userId, itemId);

            Assert.IsTrue(result);
            _catalogRepoMock.Verify(repo => repo.UpdateItem(It.Is<Item>(i => i.IsAvailable == false)), Times.Once());
            _eventRepoMock.Verify(repo => repo.AddEvent(It.IsAny<TakenOut>()), Times.Once());
        }

        [TestMethod]
        public void GetUserCheckedOutItems_ReturnsCheckedOutItems()
        {
            var userId = 1;
            var takenOutEvent = new TakenOut(1, DateTime.Now.AddDays(-1), userId);
            _eventRepoMock.Setup(repo => repo.GetEventsByUser(userId)).Returns(new List<Event> { takenOutEvent });

            var item = new Book(1, "Sample Book", "Publisher", false, "Author", "ISBN123");
            _catalogRepoMock.Setup(repo => repo.GetItem(1)).Returns(item);

            var items = _libraryService.GetUserCheckedOutItems(userId);

            Assert.AreEqual(1, items.Count());
            Assert.AreEqual(item, items.First());
        }

        [TestMethod]
        public void GetUnreturnedItems_ReturnsItemsNotReturned()
        {
            var userId = 1;
            var takenOutEvent = new TakenOut(1, DateTime.Now.AddDays(-1), userId);
            _eventRepoMock.Setup(repo => repo.GetEventsByUser(userId)).Returns(new List<Event> { takenOutEvent });

            var item = new Book(1, "Sample Book", "Publisher", false, "Author", "ISBN123");
            _catalogRepoMock.Setup(repo => repo.GetItem(1)).Returns(item);

            var items = _libraryService.GetUnreturnedItems(userId);

            Assert.AreEqual(1, items.Count());
            Assert.AreEqual(item, items.First());
        }
    }
}
