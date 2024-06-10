using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using Moq;
using MVVM.Model;
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
        private static readonly string[] UserTypes = { "Reader", "Librarian" };

        public void GenerateUserModels(UserViewModel userViewModel)
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(service => service.GetUser(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IUserDTO>().Object);
            UserFunctions userFunctions = new UserFunctions(_userServiceMock.Object);
            int numberOfUsers = 5;

            for (int i = 1; i <= numberOfUsers; i++)
            {
                string firstName = FirstNames[_random.Next(FirstNames.Length)];
                string lastName = LastNames[_random.Next(LastNames.Length)];
                string email = Emails[_random.Next(Emails.Length)];
                string userType = UserTypes[_random.Next(UserTypes.Length)];

                userViewModel.Users.Add(new UserDetailsViewModel(i, firstName, lastName, email, userType, userFunctions));
            }
        }

        public void GenerateItemModels(ItemViewModel itemViewModel)
        {
            string[] BookTitles = { "The Lightning Thief", "The Sea of Monsters", "The Titan's Curse", "The Battle of the Labyrinth", "The Last Olympian" };
            string[] Authors = { "Rick Riordan", "J.K. Rowling", "George R.R. Martin", "J.R.R. Tolkien", "Stephen King" };
            string[] MagazineTitles = { "National Geographic", "Scientific American", "Time", "Forbes", "Popular Science" };

            Mock<IItemService> _itemServiceMock = new Mock<IItemService>();
            _itemServiceMock.Setup(service => service.GetItem(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IItemDTO>().Object);
            ItemFunctions itemFunctions = new ItemFunctions(_itemServiceMock.Object);

            for (int i = 1; i <= 3; i++)
            {
                string title = BookTitles[_random.Next(BookTitles.Length)];
                int year = _random.Next(1990, 2024);
                string author = Authors[_random.Next(Authors.Length)];

                itemViewModel.Items.Add(new ItemDetailsViewModel(i, title, year, author, "Book", itemFunctions));
            }

            for (int i = 4; i <= 5; i++)
            {
                string title = MagazineTitles[_random.Next(MagazineTitles.Length)];
                int year = _random.Next(1990, 2024);
                string author = "Various Authors";

                itemViewModel.Items.Add(new ItemDetailsViewModel(i, title, year, author, "Magazine", itemFunctions));
            }
        }
        public void GenerateStateModels(StateViewModel stateViewModel)
        {
            Mock<IStateService> _stateServiceMock = new Mock<IStateService>();
            _stateServiceMock.Setup(service => service.GetState(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IStateDTO>().Object);
            StateFunctions stateFunctions = new StateFunctions(_stateServiceMock.Object);

            for (int i = 1; i <= 5; i++)
            {
                int quantity = _random.Next(10, 100);
                stateViewModel.StateDetails.Add(new StateDetailsViewModel(i, i, quantity, stateFunctions));
            }
        }
        public void GenerateEventModels(EventViewModel eventViewModel)
        {
            string[] EventTypes = { "Borrow", "Return" };
            Mock<IEventService> _eventServiceMock = new Mock<IEventService>();
            _eventServiceMock.Setup(service => service.GetEvent(It.IsAny<int>())).ReturnsAsync((int id) => new Mock<IEventDTO>().Object);
            EventFunctions eventFunctions = new EventFunctions(_eventServiceMock.Object);

            for (int i = 1; i <= 5; i++)
            {
                int userId = _random.Next(1, 6);
                int itemId = _random.Next(1, 6);
                string eventType = EventTypes[_random.Next(EventTypes.Length)];
                DateTime eventDate = DateTime.Now.AddDays(-_random.Next(0, 30));

                eventViewModel.Events.Add(new EventDetailsViewModel(i, userId, itemId, eventDate, eventType, eventFunctions));
            }
        }
    }
}