using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using Moq;
using MVVM.Model.Abstract;
using MVVM.ViewModel;
using System;

namespace MVVMTests
{
    internal class RandomDataGenerator : IDataGenerator
    {
        private readonly Random _random = new Random();

        private static readonly string[] FirstNames = { "Saber", "Zak", "Kit", "John", "Jane" };
        private static readonly string[] LastNames = { "Orien", "Missing", "Morris", "Doe", "Smith" };
        private static readonly string[] Emails = { "orien@gmail.com", "crystalDruid@gmail.com", "bestAgent@gmail.com", "johndoe@gmail.com", "janedoe@gmail.com" };
        private static readonly string[] UserTypes = { "Reader", "Librarian"};

        public void GenerateUserModels(IUserViewModel userViewModel)
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(service => service.GetUser(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IUserDTO>().Object);
            IUserFunctions userFunctions = IUserFunctions.CreateModelOperation(_userServiceMock.Object);

            int numberOfUsers = 5;

            for (int i = 1; i <= numberOfUsers; i++)
            {
                string firstName = FirstNames[_random.Next(FirstNames.Length)];
                string lastName = LastNames[_random.Next(LastNames.Length)];
                string email = Emails[_random.Next(Emails.Length)];
                string userType = UserTypes[_random.Next(UserTypes.Length)];

                userViewModel.Users.Add(IUserDetailsViewModel.CreateViewModel(i, firstName, lastName, email, userType, userFunctions));
            }
        }

        public void GenerateItemModels(IItemViewModel itemViewModel)
        {
            string[] BookTitles = { "The Lightning Thief", "The Sea of Monsters", "The Titan's Curse", "The Battle of the Labyrinth", "The Last Olympian" };
            string[] Authors = { "Rick Riordan", "J.K. Rowling", "George R.R. Martin", "J.R.R. Tolkien", "Stephen King" };
            string[] MagazineTitles = { "National Geographic", "Scientific American", "Time", "Forbes", "Popular Science" };

            Mock<IItemService> _itemServiceMock = new Mock<IItemService>();
            _itemServiceMock.Setup(service => service.GetItem(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IItemDTO>().Object);
            IItemFunctions itemFunctions = IItemFunctions.CreateModelOperation(_itemServiceMock.Object);

            for (int i = 1; i <= 3; i++)
            {
                string title = BookTitles[_random.Next(BookTitles.Length)];
                int year = _random.Next(1990, 2024);
                string author = Authors[_random.Next(Authors.Length)];

                itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(i, title, year, author, "Book", itemFunctions));
            }

            for (int i = 4; i <= 5; i++)
            {
                string title = MagazineTitles[_random.Next(MagazineTitles.Length)];
                int year = _random.Next(1990, 2024);
                string author = "Various Authors";

                itemViewModel.Items.Add(IItemDetailsViewModel.CreateViewModel(i, title, year, author, "Magazine", itemFunctions));
            }
        }
        public void GenerateStateModels(IStateViewModel stateViewModel)
        {
            Mock<IStateService> _stateServiceMock = new Mock<IStateService>();
            _stateServiceMock.Setup(service => service.GetState(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IStateDTO>().Object);
            IStateFunctions stateFunctions = IStateFunctions.CreateStateService(_stateServiceMock.Object);

            for (int i = 1; i <= 5; i++)
            {
                int quantity = _random.Next(10, 100); 
                stateViewModel.StateDetails.Add(IStateDetailsViewModel.CreateViewModel(i, i, quantity, stateFunctions));
            }
        }
        public void GenerateEventModels(IEventViewModel eventViewModel)
        {
            string[] EventTypes = { "Borrow", "Return" };
            Mock<IEventService> _eventServiceMock = new Mock<IEventService>();
            _eventServiceMock.Setup(service => service.GetEvent(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IEventDTO>().Object);
            IEventFunctions eventFunctions = IEventFunctions.CreateEventFunctions(_eventServiceMock.Object);

            for (int i = 1; i <= 5; i++)
            {
                int userId = _random.Next(1, 6); 
                int itemId = _random.Next(1, 6); 
                string eventType = EventTypes[_random.Next(EventTypes.Length)];
                DateTime eventDate = DateTime.Now.AddDays(-_random.Next(0, 30));

                eventViewModel.Events.Add(IEventDetailsViewModel.CreateViewModel(i, userId, itemId, eventDate, eventType, eventFunctions));
            }
        }
    }
}
