using LibraryData.Models;

namespace LibraryDataLayerTests
{
    internal class RandomizedTestDataGenerator : ITestDataGenerator
    {
        private Random _random = new Random();

        public List<Item> GenerateItems(int count)
        {
            var items = new List<Item>();
            for (int i = 1; i <= count; i++)
            {
                if (_random.NextDouble() > 0.5)
                {
                    items.Add(new Book(
                        i,
                        $"Book Title {i}",
                        $"Publisher {i}",
                        _random.NextDouble() > 0.5,
                        $"Author {i}",
                        $"ISBN{i}"));
                }
                else
                {
                    items.Add(new Magazine(
                        i,
                        $"Magazine Title {i}",
                        $"Publisher {i}",
                        _random.NextDouble() > 0.5,
                        i,
                        $"Month {i % 12 + 1}"));
                }
            }
            return items;
        }

        public List<User> GenerateUsers(int count)
        {
            var users = new List<User>();
            for (int i = 1; i <= count; i++)
            {
                if (_random.NextDouble() > 0.5)
                {
                    users.Add(new Librarian(
                        i,
                        $"Librarian FirstName {i}",
                        $"Librarian LastName {i}"));
                }
                else
                {
                    users.Add(new Reader(
                        i,
                        $"Reader FirstName {i}",
                        $"Reader LastName {i}"));
                }
            }
            return users;
        }

        public List<Event> GenerateEvents(int count, List<Item> items, List<User> users)
        {
            var events = new List<Event>();
            for (int i = 1; i <= count; i++)
            {
                if (_random.NextDouble() > 0.5)
                {
                    events.Add(new TakenOut(
                        items[_random.Next(items.Count)].Id,
                        DateTime.Now.AddDays(-_random.Next(1, 100)),
                        users[_random.Next(users.Count)].Id));
                }
                else
                {
                    events.Add(new Returned(
                        items[_random.Next(items.Count)].Id,
                        DateTime.Now.AddDays(-_random.Next(1, 100)),
                        users[_random.Next(users.Count)].Id,
                        "Good"));
                }
            }
            return events;
        }
    }
}
