using MVVM.Model;
using System.Collections.Generic;

namespace MVVMTests
{
    internal class ScriptedDataGenerator : IDataGenerator
    {
        public Dictionary<int, UserModel> GenerateUserModels()
        {
            return new Dictionary<int, UserModel>
            {
                { 1, new UserModel(1, "John", "Doe", "john.doe@example.com", "User") },
                { 2, new UserModel(2, "Jane", "Doe", "jane.doe@example.com", "Admin") }
            };
        }

        public Dictionary<int, ItemModel> GenerateItemModels()
        {
            return new Dictionary<int, ItemModel>
            {
                { 1, new ItemModel(1, "Title1", 2020, "Author1", "Book") },
                { 2, new ItemModel(2, "Title2", 2021, "Author2", "Journal") }
            };
        }

        public Dictionary<int, StateModel> GenerateStateModels()
        {
            return new Dictionary<int, StateModel>
            {
                { 1, new StateModel(1, 1, 100) },
                { 2, new StateModel(2, 2, 200) }
            };
        }

        public Dictionary<int, EventModel> GenerateEventModels()
        {
            return new Dictionary<int, EventModel>
            {
                { 1, new EventModel(1, 1, 1, DateTime.Now, "Borrow") },
                { 2, new EventModel(2, 2, 2, DateTime.Now, "Return") }
            };
        }
    }
}
