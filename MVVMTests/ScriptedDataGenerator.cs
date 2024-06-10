using MVVM.Model;
using MVVM.ViewModel;
using Moq;
using Logic.Services_Abstract;
using Logic.DTOs_Abstract;

namespace MVVMTests
{
    internal class ScriptedDataGenerator : IDataGenerator
    {
        public void GenerateUserModels(UserViewModel userViewModel)
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(service => service.GetUser(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IUserDTO>().Object);
            UserFunctions userFunctions = new UserFunctions(_userServiceMock.Object);
            userViewModel.Users.Add(new UserDetailsViewModel(1, "Saber", "Orien", "orien@gmail.com", "Reader", userFunctions));
            userViewModel.Users.Add(new UserDetailsViewModel(2, "Zak", "Missing", "crystalDruid@gmail.com", "Reader", userFunctions));
            userViewModel.Users.Add(new UserDetailsViewModel(3, "Kit", "Morris", "bestAgent@gmail.com", "Librarian", userFunctions));
            userViewModel.Users.Add(new UserDetailsViewModel(4, "John", "Doe", "johndoe@gmail.com", "Librarian", userFunctions));
            userViewModel.Users.Add(new UserDetailsViewModel(5, "Jane", "Smith", "janedoe@gmail.com", "Librarian", userFunctions));
        }

        public void GenerateItemModels(ItemViewModel itemViewModel)
        {
            Mock<IItemService> _itemServiceMock = new Mock<IItemService>();
            _itemServiceMock.Setup(service => service.GetItem(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IItemDTO>().Object);
            ItemFunctions itemFunctions = new ItemFunctions(_itemServiceMock.Object);

            itemViewModel.Items.Add(new ItemDetailsViewModel(1, "The Lightning Thief", 2005, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(new ItemDetailsViewModel(2, "The Sea of Monsters", 2006, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(new ItemDetailsViewModel(3, "The Titan's Curse", 2007, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(new ItemDetailsViewModel(4, "National Geographic", 2023, "National Geographic Society", "Magazine", itemFunctions));
            itemViewModel.Items.Add(new ItemDetailsViewModel(5, "Scientific American", 2023, "Scientific American", "Magazine", itemFunctions));
        }
        public void GenerateStateModels(StateViewModel stateViewModel)
        {
            Mock<IStateService> _stateServiceMock = new Mock<IStateService>();
            _stateServiceMock.Setup(service => service.GetState(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IStateDTO>().Object);
            StateFunctions stateFunctions = new StateFunctions(_stateServiceMock.Object);

            stateViewModel.StateDetails.Add(new StateDetailsViewModel(1, 1, 19, stateFunctions));
            stateViewModel.StateDetails.Add(new StateDetailsViewModel(2, 2, 29, stateFunctions));
            stateViewModel.StateDetails.Add(new StateDetailsViewModel(3, 3, 39, stateFunctions));
            stateViewModel.StateDetails.Add(new StateDetailsViewModel(4, 4, 49, stateFunctions));
            stateViewModel.StateDetails.Add(new StateDetailsViewModel(5, 5, 59, stateFunctions));
        }
        public void GenerateEventModels(EventViewModel eventViewModel)
        {
            Mock<IEventService> _eventServiceMock = new Mock<IEventService>();
            _eventServiceMock.Setup(service => service.GetEvent(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IEventDTO>().Object);
            EventFunctions eventFunctions = new EventFunctions(_eventServiceMock.Object);

            eventViewModel.Events.Add(new EventDetailsViewModel(1, 1, 1, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(new EventDetailsViewModel(1, 1, 1, DateTime.Now, "Return", eventFunctions));
            eventViewModel.Events.Add(new EventDetailsViewModel(1, 2, 1, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(new EventDetailsViewModel(1, 2, 2, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(new EventDetailsViewModel(1, 2, 2, DateTime.Now, "Return", eventFunctions));
        }
    }
}