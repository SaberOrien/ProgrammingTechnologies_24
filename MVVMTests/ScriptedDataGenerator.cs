using MVVM.Model.Abstract;
using MVVM.ViewModel;
using Moq;
using Logic.Services_Abstract;
using Logic.DTOs_Abstract;

namespace MVVMTests
{
    internal class ScriptedDataGenerator : IDataGenerator
    {
        public void GenerateUserModels(IUserViewModel userViewModel)
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(service => service.GetUser(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IUserDTO>().Object);
            IUserFunctions userFunctions = IUserFunctions.CreateModelOperation(_userServiceMock.Object);
            userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(1, "Saber", "Orien", "orien@gmail.com", "Reader", userFunctions));
            userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(2, "Zak", "Missing", "crystalDruid@gmail.com", "Reader", userFunctions));
            userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(3, "Kit", "Morris", "bestAgent@gmail.com", "Librarian", userFunctions));
            userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(4, "John", "Doe", "johndoe@gmail.com", "Librarian", userFunctions));
            userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(5, "Jane", "Smith", "janedoe@gmail.com", "Librarian", userFunctions));
        }

        public void GenerateItemModels(IItemViewModel itemViewModel)
        {
            Mock<IItemService> _itemServiceMock = new Mock<IItemService>();
            _itemServiceMock.Setup(service => service.GetItem(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IItemDTO>().Object);
            IItemFunctions itemFunctions = IItemFunctions.CreateModelOperation(_itemServiceMock.Object);

            itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(1, "The Lightning Thief", 2005, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(2, "The Sea of Monsters", 2006, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(3, "The Titan's Curse", 2007, "Rick Riordan", "Book", itemFunctions));
            itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(4, "National Geographic", 2023, "National Geographic Society", "Magazine", itemFunctions));
            itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(5, "Scientific American", 2023, "Scientific American", "Magazine", itemFunctions));
        }
        public void GenerateStateModels(IStateViewModel stateViewModel)
        {
            Mock<IStateService> _stateServiceMock = new Mock<IStateService>();
            _stateServiceMock.Setup(service => service.GetState(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IStateDTO>().Object);
            IStateFunctions stateFunctions = IStateFunctions.CreateStateService(_stateServiceMock.Object);

            stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(1, 1, 19, stateFunctions));
            stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(2, 2, 29, stateFunctions));
            stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(3, 3, 39, stateFunctions));
            stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(4, 4, 49, stateFunctions));
            stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(5, 5, 59, stateFunctions));
        }
        public void GenerateEventModels(IEventViewModel eventViewModel)
        {
            Mock<IEventService> _eventServiceMock = new Mock<IEventService>();
            _eventServiceMock.Setup(service => service.GetEvent(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IEventDTO>().Object);
            IEventFunctions eventFunctions = IEventFunctions.CreateEventFunctions(_eventServiceMock.Object);

            eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(1, 1, 1, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(1, 1, 1, DateTime.Now, "Return", eventFunctions));
            eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(1, 2, 1, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(1, 2, 2, DateTime.Now, "Borrow", eventFunctions));
            eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(1, 2, 2, DateTime.Now, "Return", eventFunctions));
        }
    }
}
