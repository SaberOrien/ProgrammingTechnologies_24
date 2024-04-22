using LibraryData.Models;

namespace LibraryDataLayerTests
{
    internal class ScriptedTestDataGenerator : ITestDataGenerator
    {
        public List<Item> GenerateItems(int count)
        {
            var items = new List<Item>();
            for (int i = 1; i <= count; i++)
            {
                if (i % 2 == 0)
                {
                    items.Add(new Book(i, $"Book Title {i}", $"Publisher {i}", true, $"Author {i}", $"ISBN{i}"));
                }
                else
                {
                    items.Add(new Magazine(i, $"Magazine Title {i}", $"Publisher {i}", true, i, $"Month {i % 12 + 1}"));
                }
            }
            return items;
        }

        public List<User> GenerateUsers(int count)
        {
            var users = new List<User>();
            for (int i = 1; i <= count; i++)
            {
                if (i % 2 == 0)
                {
                    users.Add(new Librarian(i, $"FirstName {i}", $"LastName {i}"));
                }
                else
                {
                    users.Add(new Reader(i, $"FirstName {i}", $"LastName {i}"));
                }
            }
            return users;
        }

        public List<Event> GenerateEvents(int count, List<Item> items, List<User> users)
        {
            var events = new List<Event>();
            for (int i = 1; i <= count; i++)
            {
                if (i % 2 == 0)
                {
                    events.Add(new Returned(items[i % items.Count].Id, DateTime.Now.AddDays(-i), users[i % users.Count].Id, "Good"));
                }
                else
                {
                    events.Add(new TakenOut(items[i % items.Count].Id, DateTime.Now.AddDays(-i), users[i % users.Count].Id));
                }
            }
            return events;
        }
    }
}
