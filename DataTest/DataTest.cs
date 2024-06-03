using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.AbstractInterfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        private static string connectionString;
        private readonly IDataRepository _dataRepository = IDataRepository.CreateDatabase(IDataContext.CreateContext(connectionString));

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            string _DBRelativePath = @"DataTestDB.mdf";
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string _DBPath = Path.Combine(_projectRootDir, "DataTest", _DBRelativePath);

            FileInfo _databaseFile = new FileInfo(_DBPath);
            Assert.IsTrue(_databaseFile.Exists, $"{_DBPath} does not exist!");

            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True;Connect Timeout=30;";
        }

        [TestMethod]
        public async Task TestUser()
        {
            int userId = 1;
            #region Add
            await _dataRepository.AddUser(userId, "Saber", "Orien", "orien@gmail.com", "Reader");
            IUser user = await _dataRepository.GetUser(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual("Saber", user.Name);
            Assert.AreEqual("Orien", user.Surname);
            Assert.AreEqual("orien@gmail.com", user.Email);
            Assert.AreEqual("Reader", user.UserType);
            #endregion
            var users = await _dataRepository.GetAllUsers();
            Assert.IsTrue(users.Count > 0);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUser(userId + 2));
            #region Update
            await _dataRepository.UpdateUser(userId, "Percy", "Jackson", "seaweed@gmail.com", "Librarian");
            IUser updatedUser = await _dataRepository.GetUser(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, updatedUser.Id);
            Assert.AreEqual("Percy", updatedUser.Name);
            Assert.AreEqual("Jackson", updatedUser.Surname);
            Assert.AreEqual("seaweed@gmail.com", updatedUser.Email);
            Assert.AreEqual("Librarian", updatedUser.UserType);
            #endregion

            #region Delete
            await _dataRepository.DeleteUser(userId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUser(userId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteUser(userId));
            #endregion
        }

        [TestMethod]
        public async Task TestItem()
        {
            int itemId = 1;
            await _dataRepository.AddItem(itemId, "Three Mages and a margarita", 2018, "Annette Marie", "Book");
            IItem book = await _dataRepository.GetItem(itemId);

            Assert.IsNotNull(book);
            Assert.AreEqual(itemId, book.Id);
            Assert.AreEqual("Three Mages and a margarita", book.Title);
            Assert.AreEqual(2018, book.PublicationYear);
            Assert.AreEqual("Annette Marie", book.Author);
            Assert.AreEqual("Book", book.ItemType);

            var items = await _dataRepository.GetAllItems();
            Assert.AreEqual(items.Count, 1);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetItem(2));

            await _dataRepository.UpdateItem(itemId, "The Lightning Thief", 2005, "Rick Riordan", "Book");
            IItem updatedItem = await _dataRepository.GetItem(itemId);

            Assert.IsNotNull(updatedItem);
            Assert.AreEqual(itemId, updatedItem.Id);
            Assert.AreEqual("The Lightning Thief", updatedItem.Title);
            Assert.AreEqual(2005, updatedItem.PublicationYear);
            Assert.AreEqual("Rick Riordan", updatedItem.Author);
            Assert.AreEqual("Book", updatedItem.ItemType);

            await _dataRepository.DeleteItem(itemId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetItem(itemId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteItem(itemId));

        }

        [TestMethod]
        public async Task TestState()
        {
            int itemId = 1;
            int stateId = 1;

            await _dataRepository.AddItem(itemId, "The Lightning Thief", 2005, "Rick Riordan", "Book");
            IItem item = await _dataRepository.GetItem(itemId);

            await _dataRepository.AddState(stateId, itemId, 7);
            IState state = await _dataRepository.GetState(itemId);

            Assert.IsNotNull(state);
            Assert.AreEqual(stateId, state.Id);
            Assert.AreEqual(itemId, state.ItemId);
            Assert.AreEqual(7, state.ItemAmount);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateState(stateId, itemId, -12));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateState(stateId, itemId + 2, 7));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateState(stateId + 2, itemId, 7));


            await _dataRepository.DeleteState(stateId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetState(stateId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteState(stateId));
            await _dataRepository.DeleteItem(itemId);
        }

        [TestMethod]
        public async Task TestEvent()
        {
            int itemId = 2;
            int userId = 2;
            int stateId = 2;
            int eventId = 2;

            await _dataRepository.AddItem(itemId, "The Lightning Thief", 2005, "Rick Riordan", "Book");
            await _dataRepository.AddState(stateId, itemId, 7);
            await _dataRepository.AddUser(userId, "Percy", "Jackson", "seaweed@gmail.com", "Reader");

            IItem item = await _dataRepository.GetItem(itemId);
            IState state = await _dataRepository.GetState(stateId);
            IUser user = await _dataRepository.GetUser(userId);

            state = await _dataRepository.GetState(stateId);
            Assert.AreEqual(7, state.ItemAmount);

            await _dataRepository.AddEvent(eventId, stateId, userId, "Borrow"); //here add one event
            IEvent @event = await _dataRepository.GetEvent(eventId);

            Assert.IsNotNull(@event);
            Assert.AreEqual(eventId, @event.Id);
            Assert.AreEqual(stateId, @event.StateId);
            Assert.AreEqual(userId, @event.UserId);

            var events = await _dataRepository.GetAllEvents();
            Assert.AreEqual(1, events.Count);

            state = await _dataRepository.GetState(stateId);
            Assert.AreEqual(6, state.ItemAmount);

            await _dataRepository.UpdateEvent(eventId, stateId, userId, DateTime.Now, "Borrow"); //does it add new event?
            IEvent updateEvent = await _dataRepository.GetEvent(eventId);
            Assert.IsNotNull(updateEvent);
            Assert.AreEqual(eventId, updateEvent.Id);
            Assert.AreEqual(stateId, updateEvent.StateId);
            Assert.AreEqual(userId, updateEvent.UserId);

            int returnEventId = 3;
            await _dataRepository.AddEvent(returnEventId, stateId, userId, "Return");
            IEvent returnEvent = await _dataRepository.GetEvent(returnEventId);
            Assert.IsNotNull(returnEvent);
            Assert.AreEqual(returnEventId, returnEvent.Id);
            Assert.AreEqual(stateId, returnEvent.StateId);
            Assert.AreEqual(userId, returnEvent.UserId);
            events = await _dataRepository.GetAllEvents();
            Assert.AreEqual(2, events.Count);
            state = await _dataRepository.GetState(stateId);
            Assert.AreEqual(7, state.ItemAmount);

            await _dataRepository.DeleteEvent(eventId);
            await _dataRepository.DeleteEvent(returnEventId);
            events = await _dataRepository.GetAllEvents();
            Assert.AreEqual(0, events.Count);
            await _dataRepository.DeleteState(stateId);
            await _dataRepository.DeleteItem(itemId);
            await _dataRepository.DeleteUser(userId);
        }
    }
}
