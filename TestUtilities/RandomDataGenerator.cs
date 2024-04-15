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
    }
}
