namespace MVVMTests
{
    using Moq;
    using MVVM.Model;
    using MVVM.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class MVVMTests
    {
        private IDataGenerator dataGenerator;

        private Mock<IUserFunctions> mockUserFunctions;
        private UserViewModel userViewModel;
        private UserDetailsViewModel userDetailsViewModel;

        private Mock<IItemFunctions> mockItemFunctions;
        private ItemViewModel itemViewModel;
        private ItemDetailsViewModel itemDetailsViewModel;

        private Mock<IStateFunctions> mockStateFunctions;
        private StateViewModel stateViewModel;
        private StateDetailsViewModel stateDetailsViewModel;

        private Mock<IEventFunctions> mockEventFunctions;
        private EventViewModel eventViewModel;
        private EventDetailsViewModel eventDetailsViewModel;

        [TestInitialize]
        public void SetUp()
        {
            dataGenerator = new ScriptedDataGenerator(); // Or new RandomDataGenerator();

            mockUserFunctions = GenerateMockUserFunctions(dataGenerator);
            mockItemFunctions = GenerateMockItemFunctions(dataGenerator);
            mockStateFunctions = GenerateMockStateFunctions(dataGenerator);
            mockEventFunctions = GenerateMockEventFunctions(dataGenerator);

            userViewModel = new UserViewModel(mockUserFunctions.Object);
            userDetailsViewModel = new UserDetailsViewModel(mockUserFunctions.Object);

            itemViewModel = new ItemViewModel(mockItemFunctions.Object);
            itemDetailsViewModel = new ItemDetailsViewModel(mockItemFunctions.Object);

            stateViewModel = new StateViewModel(mockStateFunctions.Object);
            stateDetailsViewModel = new StateDetailsViewModel(mockStateFunctions.Object);

            eventViewModel = new EventViewModel(mockEventFunctions.Object);
            eventDetailsViewModel = new EventDetailsViewModel(mockEventFunctions.Object);
        }

        private Mock<IUserFunctions> GenerateMockUserFunctions(IDataGenerator generator)
        {
            var mock = new Mock<IUserFunctions>();

            mock.Setup(uf => uf.GetUser(It.IsAny<int>()))
                .ReturnsAsync((int id) => generator.GenerateUserModels().ContainsKey(id) ? generator.GenerateUserModels()[id] : null);

            mock.Setup(uf => uf.GetUsers())
                .ReturnsAsync(generator.GenerateUserModels());

            return mock;
        }

        private Mock<IItemFunctions> GenerateMockItemFunctions(IDataGenerator generator)
        {
            var mock = new Mock<IItemFunctions>();

            mock.Setup(itf => itf.GetItem(It.IsAny<int>()))
                .ReturnsAsync((int id) => generator.GenerateItemModels().ContainsKey(id) ? generator.GenerateItemModels()[id] : null);

            mock.Setup(itf => itf.GetItems())
                .ReturnsAsync(generator.GenerateItemModels());

            return mock;
        }

        private Mock<IStateFunctions> GenerateMockStateFunctions(IDataGenerator generator)
        {
            var mock = new Mock<IStateFunctions>();

            mock.Setup(sf => sf.GetState(It.IsAny<int>()))
                .ReturnsAsync((int id) => generator.GenerateStateModels().ContainsKey(id) ? generator.GenerateStateModels()[id] : null);

            mock.Setup(sf => sf.GetAllStates())
                .ReturnsAsync(generator.GenerateStateModels());

            return mock;
        }

        private Mock<IEventFunctions> GenerateMockEventFunctions(IDataGenerator generator)
        {
            var mock = new Mock<IEventFunctions>();

            mock.Setup(ef => ef.GetEvent(It.IsAny<int>()))
                .ReturnsAsync((int id) => generator.GenerateEventModels().ContainsKey(id) ? generator.GenerateEventModels()[id] : null);

            mock.Setup(ef => ef.GetAllEvents())
                .ReturnsAsync(generator.GenerateEventModels());

            return mock;
        }

        [TestMethod]
        public void CreateUserCommand_ShouldBeExecutable()
        {
            // Arrange
            userViewModel.Name = "John";
            userViewModel.Surname = "Doe";
            userViewModel.Email = "john.doe@example.com";
            userViewModel.UserType = "User";

            // Act & Assert
            Assert.IsTrue(userViewModel.CreateUser.CanExecute(null));
        }

        [TestMethod]
        public void DeleteUserCommand_ShouldBeExecutable()
        {
            // Arrange
            userViewModel.Name = "John";
            userViewModel.Surname = "Doe";
            userViewModel.Email = "john.doe@example.com";
            userViewModel.UserType = "User";

            // Act & Assert
            Assert.IsTrue(userViewModel.RemoveUser.CanExecute(null));
        }

        [TestMethod]
        public void UpdateUserCommand_ShouldBeExecutable()
        {
            // Arrange
            userDetailsViewModel.Name = "John";
            userDetailsViewModel.Surname = "Doe";
            userDetailsViewModel.Email = "john.doe@example.com";
            userDetailsViewModel.UserType = "User";

            // Act & Assert
            Assert.IsTrue(userDetailsViewModel.UpdateUser.CanExecute(null));
        }

        [TestMethod]
        public void CreateItemCommand_ShouldBeExecutable()
        {
            // Arrange
            itemViewModel.Title = "Sample Title";
            itemViewModel.PublicationYear = 2021;
            itemViewModel.Author = "Sample Author";
            itemViewModel.ItemType = "Book";

            // Act & Assert
            Assert.IsTrue(itemViewModel.CreateItem.CanExecute(null));
        }

        [TestMethod]
        public void DeleteItemCommand_ShouldBeExecutable()
        {
            // Arrange
            itemViewModel.Title = "Sample Title";
            itemViewModel.PublicationYear = 2021;
            itemViewModel.Author = "Sample Author";
            itemViewModel.ItemType = "Book";
            itemViewModel.Details = new ItemDetailsViewModel(1, "Sample Title", 2021, "Sample Author", "Book", mockItemFunctions.Object);

            // Act & Assert
            Assert.IsTrue(itemViewModel.RemoveItem.CanExecute(null));
        }

        [TestMethod]
        public void UpdateItemCommand_ShouldBeExecutable()
        {
            // Arrange
            itemDetailsViewModel.Id = 1;
            itemDetailsViewModel.Title = "Sample Title";
            itemDetailsViewModel.PublicationYear = 2021;
            itemDetailsViewModel.Author = "Sample Author";
            itemDetailsViewModel.ItemType = "Book";

            // Act & Assert
            Assert.IsTrue(itemDetailsViewModel.UpdateItem.CanExecute(null));
        }

        [TestMethod]
        public void CreateStateCommand_ShouldBeExecutable()
        {
            // Arrange
            stateViewModel.ItemId = 1;
            stateViewModel.ItemAmount = 100;

            // Act & Assert
            Assert.IsTrue(stateViewModel.CreateState.CanExecute(null));
        }

        [TestMethod]
        public void DeleteStateCommand_ShouldBeExecutable()
        {
            // Arrange
            stateViewModel.ItemId = 1;
            stateViewModel.ItemAmount = 100;
            stateViewModel.StateDetailsViewModel = new StateDetailsViewModel(1, 1, 100, mockStateFunctions.Object);

            // Act & Assert
            Assert.IsTrue(stateViewModel.RemoveState.CanExecute(null));
        }

        [TestMethod]
        public void UpdateStateCommand_ShouldBeExecutable()
        {
            // Arrange
            stateDetailsViewModel.Id = 1;
            stateDetailsViewModel.ItemId = 1;
            stateDetailsViewModel.ItemAmount = 100;

            // Act & Assert
            Assert.IsTrue(stateDetailsViewModel.UpdateState.CanExecute(null));
        }

        [TestMethod]
        public void CreateEventCommand_ShouldBeExecutable()
        {
            // Arrange
            eventViewModel.StateId = 1;
            eventViewModel.UserId = 1;

            // Act & Assert
            Assert.IsTrue(eventViewModel.BorrowEvent.CanExecute(null));
            Assert.IsTrue(eventViewModel.ReturnEvent.CanExecute(null));
        }

        [TestMethod]
        public void DeleteEventCommand_ShouldBeExecutable()
        {
            // Arrange
            eventViewModel.StateId = 1;
            eventViewModel.UserId = 1;
            eventViewModel.EventDetailsViewModel = new EventDetailsViewModel(1, 1, 1, DateTime.Now, "Borrow", mockEventFunctions.Object);

            // Act & Assert
            Assert.IsTrue(eventViewModel.RemoveEvent.CanExecute(null));
        }

        [TestMethod]
        public void UpdateEventCommand_ShouldBeExecutable()
        {
            // Arrange
            eventDetailsViewModel.Id = 1;
            eventDetailsViewModel.StateId = 1;
            eventDetailsViewModel.UserId = 1;
            eventDetailsViewModel.DateStamp = DateTime.Now;
            eventDetailsViewModel.EventType = "Borrow";

            // Act & Assert
            Assert.IsTrue(eventDetailsViewModel.UpdateEvent.CanExecute(null));
        }
    }
}
