using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.TestUtilities
{
    public class RandomDataGenerator
    {
        private readonly Random _random = new Random();

        public List<Item> GenerateRandomItems(int numberOfItems)
        {
            var items = new List<Item>();
            for (int i = 0; i < numberOfItems; i++)
            {
                var item = new Item
                {
                    Id = i,
                    Title = $"Book {i}",
                    Author = $"Author {_random.Next(1, 100)}",
                    PublishingYear = _random.Next(1900, 2023),
                    Publisher = $"Publisher {_random.Next(1, 50)}"
                };
                items.Add(item);
            }
            return items;
        }

        public List<Reader> GenerateRandomReaders(int numberOfReaders)
        {
            var readers = new List<Reader>();
            for (int i = 0; i < numberOfReaders; i++)
            {
                var reader = new Reader
                {
                    Id = i,
                    Name = $"ReaderName{i}",
                    Surname = $"ReaderSurname{i}",
                    DateOfSignUp = DateTime.Now.AddDays(-_random.Next(0, 365))
                };
                readers.Add(reader);
            }
            return readers;
        }

        public List<Librarian> GenerateRandomLibrarians(int numberOfLibrarians)
        {
            var librarians = new List<Librarian>();
            for (int i = 0; i < numberOfLibrarians; i++)
            {
                var librarian = new Librarian
                {
                    Id = i,
                    Name = $"LibrarianName{i}",
                    Surname = $"LibrarianSurname{i}"
                };
                librarians.Add(librarian);
            }
            return librarians;
        }

        public List<Event> GenerateRandomEvents(int numberOfEvents, List<int> itemIds)
        {
            var events = new List<Event>();
            for (int i = 0; i < numberOfEvents; i++)
            {
                var newEvent = new Event
                {
                    UserId = _random.Next(1, 100),
                    ItemId = itemIds[_random.Next(itemIds.Count)],
                    Timestamp = DateTime.Now.AddDays(-_random.Next(0, 365)),
                    Type = (EventType) _random.Next(0, 2)
                };
                events.Add(newEvent);
            }
            return events;
        }
    }
}
