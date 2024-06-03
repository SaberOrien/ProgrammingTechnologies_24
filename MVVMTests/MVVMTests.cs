using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using Moq;
using MVVM.Model.Abstract;
using MVVM.ViewModel;
using MVVM.ViewModel.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMTests
{
    [TestClass]
    public class MVVMTests
    {
        private IUserFunctions userFunctions;
        private IUserViewModel userViewModel;

        private IItemFunctions itemFunctions;
        private IItemViewModel itemViewModel;

        private IStateFunctions stateFunctions;
        private IStateViewModel stateViewModel;

        private IEventFunctions eventFunctions;
        private IEventViewModel eventViewModel;

        [TestInitialize]
        public void SetUp()
        {
            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(service => service.GetUser(It.IsAny<int>())).ReturnsAsync(new Mock<IUserDTO>().Object);
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(new Dictionary<int, IUserDTO>
            {
                { 1, new Mock<IUserDTO>().Object },
                { 2, new Mock<IUserDTO>().Object },
                { 3, new Mock<IUserDTO>().Object },
                { 4, new Mock<IUserDTO>().Object },
                { 5, new Mock<IUserDTO>().Object }
            });

            this.userFunctions = IUserFunctions.CreateModelOperation(mockUserService.Object);
            this.userViewModel = IUserViewModel.CreateViewModel(this.userFunctions);


            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(service => service.GetItem(It.IsAny<int>())).ReturnsAsync(new Mock<IItemDTO>().Object);
            mockItemService.Setup(service => service.GetItems()).ReturnsAsync(new Dictionary<int, IItemDTO>
            {
                { 1, new Mock<IItemDTO>().Object },
                { 2, new Mock<IItemDTO>().Object },
                { 3, new Mock<IItemDTO>().Object },
                { 4, new Mock<IItemDTO>().Object },
                { 5, new Mock<IItemDTO>().Object }
            });

            this.itemFunctions = IItemFunctions.CreateModelOperation(mockItemService.Object);
            this.itemViewModel = IItemViewModel.CreateViewModel(this.itemFunctions);

            var mockStateService = new Mock<IStateService>();
            mockStateService.Setup(service => service.GetState(It.IsAny<int>())).ReturnsAsync(new Mock<IStateDTO>().Object);
            mockStateService.Setup(service => service.GetAllStates()).ReturnsAsync(new Dictionary<int, IStateDTO>
            {
                { 1, new Mock<IStateDTO>().Object },
                { 2, new Mock<IStateDTO>().Object },
                { 3, new Mock<IStateDTO>().Object },
                { 4, new Mock<IStateDTO>().Object },
                { 5, new Mock<IStateDTO>().Object }
            });

            this.stateFunctions = IStateFunctions.CreateStateService(mockStateService.Object);
            this.stateViewModel = IStateViewModel.CreateViewModel(this.stateFunctions);

            var mockEventService = new Mock<IEventService>();
            mockEventService.Setup(service => service.GetEvent(It.IsAny<int>())).ReturnsAsync(new Mock<IEventDTO>().Object);
            mockEventService.Setup(service => service.GetAllEvents()).ReturnsAsync(new Dictionary<int, IEventDTO>
            {
                { 1, new Mock<IEventDTO>().Object },
                { 2, new Mock<IEventDTO>().Object },
                { 3, new Mock<IEventDTO>().Object },
                { 4, new Mock<IEventDTO>().Object },
                { 5, new Mock<IEventDTO>().Object }
            });

            this.eventFunctions = IEventFunctions.CreateEventFunctions(mockEventService.Object);
            this.eventViewModel = IEventViewModel.CreateViewModel(this.eventFunctions);

            IDataGenerator data = new RandomDataGenerator();
            data.GenerateUserModels(userViewModel);
            data.GenerateItemModels(itemViewModel);
            data.GenerateStateModels(stateViewModel);
            data.GenerateEventModels(eventViewModel);
        }

        [TestMethod]
        public void GenerateUserModels_ShouldPopulateUsersList()
        {
            Assert.AreEqual(5, userViewModel.Users.Count);
            Assert.AreEqual(5, itemViewModel.Items.Count);
            Assert.AreEqual(5, stateViewModel.StateDetails.Count);
            Assert.AreEqual(5, eventViewModel.Events.Count);
        }

        [TestMethod]
        public void UserViewModelTests()
        {
            userViewModel.Name = userViewModel.Users[0].Name;
            userViewModel.Surname = userViewModel.Users[0].Surname;
            userViewModel.Email = userViewModel.Users[0].Email;
            userViewModel.UserType = userViewModel.Users[0].UserType;

            Assert.IsNotNull(userViewModel.CreateUser);
            Assert.IsTrue(userViewModel.CreateUser.CanExecute(null));

            userViewModel.Email = null;
            Assert.IsFalse(userViewModel.CreateUser.CanExecute(null));
            Assert.IsTrue(userViewModel.RemoveUser.CanExecute(null));
        }

        [TestMethod]
        public void UserDetailsViewModelTests()
        {
            userViewModel.Users[0].Surname = "NewSurname";
            Assert.IsTrue(userViewModel.Users[0].UpdateUser.CanExecute(null));
        }

        [TestMethod]
        public void ItemViewModelTests()
        {
            itemViewModel.Title = itemViewModel.Items[0].Title;
            itemViewModel.PublicationYear = itemViewModel.Items[0].PublicationYear;
            itemViewModel.Author = itemViewModel.Items[0].Author;
            itemViewModel.ItemType = itemViewModel.Items[0].ItemType;

            Assert.IsNotNull(itemViewModel.CreateItem);
            Assert.IsTrue(itemViewModel.CreateItem.CanExecute(null));

            itemViewModel.PublicationYear = -3;
            Assert.IsFalse(itemViewModel.CreateItem.CanExecute(null));
            Assert.IsTrue(itemViewModel.RemoveItem.CanExecute(null));
        }

        [TestMethod]
        public void ItemDetailsViewModelTests()
        {
            itemViewModel.Items[0].Title = "NewSurname";
            Assert.IsTrue(itemViewModel.Items[0].UpdateItem.CanExecute(null));
        }

        [TestMethod]
        public void StateViewModelTests()
        {
            stateViewModel.ItemId = stateViewModel.StateDetails[0].ItemId;
            stateViewModel.ItemAmount = stateViewModel.StateDetails[0].ItemAmount;


            Assert.IsNotNull(stateViewModel.CreateState);
            Assert.IsTrue(stateViewModel.CreateState.CanExecute(null));

            stateViewModel.ItemAmount = -3;
            Assert.IsFalse(stateViewModel.CreateState.CanExecute(null));
            Assert.IsTrue(stateViewModel.RemoveState.CanExecute(null));
        }

        [TestMethod]
        public void StateDetailsViewModelTests()
        {
            stateViewModel.StateDetails[0].ItemAmount = 10; 
            Assert.IsTrue(stateViewModel.StateDetails[0].UpdateState.CanExecute(null));
        }

        [TestMethod]
        public void EventViewModelTests()
        {
            eventViewModel.StateId = eventViewModel.Events[0].StateId;
            eventViewModel.UserId = eventViewModel.Events[0].UserId;

            Assert.IsNotNull(eventViewModel.BorrowEvent);
            Assert.IsTrue(eventViewModel.BorrowEvent.CanExecute(null));
            Assert.IsTrue(eventViewModel.ReturnEvent.CanExecute(null));
            eventViewModel.StateId = -3;
            Assert.IsFalse(eventViewModel.BorrowEvent.CanExecute(null));
            Assert.IsFalse(eventViewModel.ReturnEvent.CanExecute(null));
            Assert.IsTrue(eventViewModel.RemoveEvent.CanExecute(null));
        }

        [TestMethod]
        public void EventDetailsViewModelTests()
        {
            eventViewModel.Events[0].StateId = 10;
            Assert.IsTrue(eventViewModel.Events[0].UpdateEvent.CanExecute(null));
        }
    }
}
