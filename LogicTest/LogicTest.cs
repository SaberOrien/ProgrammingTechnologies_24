using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using Data.AbstractInterfaces;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {
        private Mock<IDataRepository> _mockRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDataRepository>();
        }

        [TestMethod]
        public async Task TestUserService()
        {
            //Setup 
            Mock<IUser> _mockUser = new Mock<IUser>();
            _mockUser.SetupGet(u => u.Id).Returns(1);
            _mockUser.SetupGet(u => u.Name).Returns("Saber");
            _mockUser.SetupGet(u => u.Surname).Returns("Orien");
            _mockUser.SetupGet(u => u.Email).Returns("orien@gmail.com");
            _mockUser.SetupGet(u => u.UserType).Returns("Reader");

            _mockRepository.Setup(r => r.GetUser(It.IsAny<int>())).ReturnsAsync(_mockUser.Object);
            _mockRepository.Setup(r => r.AddUser(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            
            IUserService userService = IUserService.CreateUserService(_mockRepository.Object);

            await userService.AddUser(1, "Saber", "Orien", "orien@gmail.com", "Reader");
            IUserDTO user = await userService.GetUser(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Saber", user.Name);
            Assert.AreEqual("Orien", user.Surname);
            Assert.AreEqual("orien@gmail.com", user.Email);
            Assert.AreEqual("Reader", user.UserType);


            _mockRepository.Verify(r => r.AddUser(1, "Saber", "Orien", "orien@gmail.com", "Reader"), Times.Once);
            _mockRepository.Verify(r => r.GetUser(1), Times.Once);

            // Update user test
            await userService.UpdateUser(1, "UpdatedName", "UpdatedSurname", "updated@gmail.com", "Admin");
            _mockUser.SetupGet(u => u.Name).Returns("UpdatedName");
            _mockUser.SetupGet(u => u.Surname).Returns("UpdatedSurname");
            _mockUser.SetupGet(u => u.Email).Returns("updated@gmail.com");
            _mockUser.SetupGet(u => u.UserType).Returns("Admin");

            user = await userService.GetUser(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("UpdatedName", user.Name);
            Assert.AreEqual("UpdatedSurname", user.Surname);
            Assert.AreEqual("updated@gmail.com", user.Email);
            Assert.AreEqual("Admin", user.UserType);

            _mockRepository.Verify(r => r.UpdateUser(1, "UpdatedName", "UpdatedSurname", "updated@gmail.com", "Admin"), Times.Once);

            // Delete user test
            await userService.DeleteUser(1);
            _mockRepository.Setup(r => r.GetUser(It.IsAny<int>())).ReturnsAsync((IUser)null);
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await userService.GetUser(1));
            _mockRepository.Verify(r => r.DeleteUser(1), Times.Once);
        }

        [TestMethod]
        public async Task TestItemService()
        {
            // Setup mock item
            Mock<IItem> _mockItem = new Mock<IItem>();
            _mockItem.SetupGet(i => i.Id).Returns(1);
            _mockItem.SetupGet(i => i.Title).Returns("Test Title");
            _mockItem.SetupGet(i => i.PublicationYear).Returns(2021);
            _mockItem.SetupGet(i => i.Author).Returns("Test Author");
            _mockItem.SetupGet(i => i.ItemType).Returns("Book");

            // Setup repository methods
            _mockRepository.Setup(r => r.GetItem(It.IsAny<int>())).ReturnsAsync(_mockItem.Object);
            _mockRepository.Setup(r => r.AddItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.UpdateItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.DeleteItem(It.IsAny<int>())).Returns(Task.CompletedTask);

            IItemService itemService = IItemService.CreateItemService(_mockRepository.Object);

            // Add item test
            await itemService.AddItem(1, "Test Title", 2021, "Test Author", "Book");
            IItemDTO item = await itemService.GetItem(1);

            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("Test Title", item.Title);
            Assert.AreEqual(2021, item.PublicationYear);
            Assert.AreEqual("Test Author", item.Author);
            Assert.AreEqual("Book", item.ItemType);

            _mockRepository.Verify(r => r.AddItem(1, "Test Title", 2021, "Test Author", "Book"), Times.Once);
            _mockRepository.Verify(r => r.GetItem(1), Times.Once);

            // Update item test
            await itemService.UpdateItem(1, "Updated Title", 2022, "Updated Author", "Magazine");
            _mockItem.SetupGet(i => i.Title).Returns("Updated Title");
            _mockItem.SetupGet(i => i.PublicationYear).Returns(2022);
            _mockItem.SetupGet(i => i.Author).Returns("Updated Author");
            _mockItem.SetupGet(i => i.ItemType).Returns("Magazine");

            item = await itemService.GetItem(1);

            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("Updated Title", item.Title);
            Assert.AreEqual(2022, item.PublicationYear);
            Assert.AreEqual("Updated Author", item.Author);
            Assert.AreEqual("Magazine", item.ItemType);

            _mockRepository.Verify(r => r.UpdateItem(1, "Updated Title", 2022, "Updated Author", "Magazine"), Times.Once);

            // Delete item test
            await itemService.DeleteItem(1);
            _mockRepository.Setup(r => r.GetItem(It.IsAny<int>())).ReturnsAsync((IItem)null);
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await itemService.GetItem(1));
            _mockRepository.Verify(r => r.DeleteItem(1), Times.Once);
        }

        [TestMethod]
        public async Task TestStateService()
        {
            Mock<IItem> _mockItem = new Mock<IItem>();
            Mock<IState> _mockState = new Mock<IState>();

            // Setup mock item
            _mockItem.SetupGet(i => i.Id).Returns(1);
            _mockItem.SetupGet(i => i.Title).Returns("Test Title");
            _mockItem.SetupGet(i => i.PublicationYear).Returns(2021);
            _mockItem.SetupGet(i => i.Author).Returns("Test Author");
            _mockItem.SetupGet(i => i.ItemType).Returns("Book");

            // Setup mock state
            _mockState.SetupGet(s => s.Id).Returns(1);
            _mockState.SetupGet(s => s.ItemId).Returns(1);
            _mockState.SetupGet(s => s.ItemAmount).Returns(10);

            // Setup repository methods
            _mockRepository.Setup(r => r.GetItem(It.IsAny<int>())).ReturnsAsync(_mockItem.Object);
            _mockRepository.Setup(r => r.AddItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.GetState(It.IsAny<int>())).ReturnsAsync(_mockState.Object);
            _mockRepository.Setup(r => r.AddState(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.UpdateState(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.DeleteState(It.IsAny<int>())).Returns(Task.CompletedTask);

            IStateService stateService = IStateService.CreateStateService(_mockRepository.Object);
            IItemService itemService = IItemService.CreateItemService(_mockRepository.Object);

            // Add item to use in state
            await itemService.AddItem(1, "Test Title", 2021, "Test Author", "Book");
            IItemDTO item = await itemService.GetItem(1);

            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("Test Title", item.Title);
            Assert.AreEqual(2021, item.PublicationYear);
            Assert.AreEqual("Test Author", item.Author);
            Assert.AreEqual("Book", item.ItemType);

            // Add state test
            await stateService.AddState(1, 1, 10);
            IStateDTO state = await stateService.GetState(1);

            Assert.IsNotNull(state);
            Assert.AreEqual(1, state.Id);
            Assert.AreEqual(1, state.ItemId);
            Assert.AreEqual(10, state.ItemAmount);

            _mockRepository.Verify(r => r.AddState(1, 1, 10), Times.Once);
            _mockRepository.Verify(r => r.GetState(1), Times.Once);

            // Update state test
            await stateService.UpdateState(1, 1, 20);
            _mockState.SetupGet(s => s.ItemAmount).Returns(20);

            state = await stateService.GetState(1);

            Assert.IsNotNull(state);
            Assert.AreEqual(1, state.Id);
            Assert.AreEqual(1, state.ItemId);
            Assert.AreEqual(20, state.ItemAmount);

            _mockRepository.Verify(r => r.UpdateState(1, 1, 20), Times.Once);

            // Delete state test
            await stateService.DeleteState(1);
            _mockRepository.Setup(r => r.GetState(It.IsAny<int>())).ReturnsAsync((IState)null);
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await stateService.GetState(1));
            _mockRepository.Verify(r => r.DeleteState(1), Times.Once);
        }

        [TestMethod]
        public async Task TestEventService()
        {
            Mock<IUser> _mockUser = new Mock<IUser>();
            Mock<IState> _mockState = new Mock<IState>();
            Mock<IEvent> _mockEvent = new Mock<IEvent>();

            // Setup mock user
            _mockUser.SetupGet(u => u.Id).Returns(1);
            _mockUser.SetupGet(u => u.Name).Returns("Saber");
            _mockUser.SetupGet(u => u.Surname).Returns("Orien");
            _mockUser.SetupGet(u => u.Email).Returns("orien@gmail.com");
            _mockUser.SetupGet(u => u.UserType).Returns("Reader");

            // Setup mock state
            _mockState.SetupGet(s => s.Id).Returns(1);
            _mockState.SetupGet(s => s.ItemId).Returns(1);
            _mockState.SetupGet(s => s.ItemAmount).Returns(10);

            // Setup mock event
            _mockEvent.SetupGet(e => e.Id).Returns(1);
            _mockEvent.SetupGet(e => e.StateId).Returns(1);
            _mockEvent.SetupGet(e => e.UserId).Returns(1);
            _mockEvent.SetupGet(e => e.DateStamp).Returns(DateTime.Now);
            _mockEvent.SetupGet(e => e.EventType).Returns("Purchase");

            // Setup repository methods
            _mockRepository.Setup(r => r.GetUser(It.IsAny<int>())).ReturnsAsync(_mockUser.Object);
            _mockRepository.Setup(r => r.GetState(It.IsAny<int>())).ReturnsAsync(_mockState.Object);
            _mockRepository.Setup(r => r.GetEvent(It.IsAny<int>())).ReturnsAsync(_mockEvent.Object);
            _mockRepository.Setup(r => r.AddEvent(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.UpdateEvent(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.DeleteEvent(It.IsAny<int>())).Returns(Task.CompletedTask);

            IEventService eventService = IEventService.CreateEventService(_mockRepository.Object);

            // Add event test
            await eventService.AddEvent(1, 1, 1, "Purchase");
            IEventDTO eventDTO = await eventService.GetEvent(1);

            Assert.IsNotNull(eventDTO);
            Assert.AreEqual(1, eventDTO.Id);
            Assert.AreEqual(1, eventDTO.StateId);
            Assert.AreEqual(1, eventDTO.UserId);
            Assert.AreEqual("Purchase", eventDTO.EventType);

            _mockRepository.Verify(r => r.AddEvent(1, 1, 1, "Purchase"), Times.Once);
            _mockRepository.Verify(r => r.GetEvent(1), Times.Once);

            // Update event test
            DateTime newDate = DateTime.Now.AddDays(1);
            await eventService.UpdateEvent(1, 1, 1, newDate, "Return");
            _mockEvent.SetupGet(e => e.DateStamp).Returns(newDate);
            _mockEvent.SetupGet(e => e.EventType).Returns("Return");

            eventDTO = await eventService.GetEvent(1);

            Assert.IsNotNull(eventDTO);
            Assert.AreEqual(1, eventDTO.Id);
            Assert.AreEqual(1, eventDTO.StateId);
            Assert.AreEqual(1, eventDTO.UserId);
            Assert.AreEqual(newDate, eventDTO.DateStamp);
            Assert.AreEqual("Return", eventDTO.EventType);

            _mockRepository.Verify(r => r.UpdateEvent(1, 1, 1, newDate, "Return"), Times.Once);

            // Delete event test
            await eventService.DeleteEvent(1);
            _mockRepository.Setup(r => r.GetEvent(It.IsAny<int>())).ReturnsAsync((IEvent)null);
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await eventService.GetEvent(1));
            _mockRepository.Verify(r => r.DeleteEvent(1), Times.Once);
        }
    }
}
