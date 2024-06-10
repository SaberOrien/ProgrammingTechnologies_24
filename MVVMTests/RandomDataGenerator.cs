using MVVM.Model;
using System;
using System.Collections.Generic;

namespace MVVMTests
{
    internal class RandomDataGenerator : IDataGenerator
    {
        private static readonly Random random = new Random();

        public Dictionary<int, UserModel> GenerateUserModels()
        {
            var users = new Dictionary<int, UserModel>();

            for (int i = 1; i <= 10; i++)
            {
                users.Add(i, new UserModel(i, GenerateRandomString(), GenerateRandomString(), GenerateRandomEmail(), GenerateRandomUserType()));
            }

            return users;
        }

        public Dictionary<int, ItemModel> GenerateItemModels()
        {
            var items = new Dictionary<int, ItemModel>();

            for (int i = 1; i <= 10; i++)
            {
                items.Add(i, new ItemModel(i, GenerateRandomString(), GenerateRandomYear(), GenerateRandomString(), GenerateRandomItemType()));
            }

            return items;
        }

        public Dictionary<int, StateModel> GenerateStateModels()
        {
            var states = new Dictionary<int, StateModel>();

            for (int i = 1; i <= 10; i++)
            {
                states.Add(i, new StateModel(i, random.Next(1, 10), random.Next(1, 1000)));
            }

            return states;
        }

        public Dictionary<int, EventModel> GenerateEventModels()
        {
            var events = new Dictionary<int, EventModel>();

            for (int i = 1; i <= 10; i++)
            {
                events.Add(i, new EventModel(i, random.Next(1, 10), random.Next(1, 10), DateTime.Now.AddDays(-random.Next(1, 100)), GenerateRandomEventType()));
            }

            return events;
        }

        private string GenerateRandomString(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        private int GenerateRandomYear()
        {
            return random.Next(1900, DateTime.Now.Year);
        }

        private string GenerateRandomEmail()
        {
            return $"{GenerateRandomString()}@example.com";
        }

        private string GenerateRandomUserType()
        {
            var userTypes = new List<string> { "User", "Admin" };
            return userTypes[random.Next(userTypes.Count)];
        }

        private string GenerateRandomItemType()
        {
            var itemTypes = new List<string> { "Book", "Journal", "Magazine", "Newspaper" };
            return itemTypes[random.Next(itemTypes.Count)];
        }

        private string GenerateRandomEventType()
        {
            var eventTypes = new List<string> { "Borrow", "Return" };
            return eventTypes[random.Next(eventTypes.Count)];
        }
    }
}
