using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Logic;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.TestUtilities
{
    public class ScenarioDataGenerator
    {
        public (List<Reader>, List<Item>, List<Event>) GenerateOverdueItemsScenario()
        {
            var readers = new List<Reader>
        {
            new Reader { Id = 20, Name = "John", Surname = "Doe", DateOfSignUp = DateTime.Today.AddDays(-400), BooksBorrowed = new List<BorrowRecord>() },
            new Reader { Id = 21, Name = "Jane", Surname = "Doe", DateOfSignUp = DateTime.Today.AddDays(-200), BooksBorrowed = new List<BorrowRecord>() }
        };

            var items = new List<Item>
        {
            new Item { Id = 101, Title = "C# Fundamentals", Author = "Author A", PublishingYear = 2015, Publisher = "Publisher X", copiesInStock = 0, copiesBorrowed = 0 },
            new Item { Id = 102, Title = "Advanced C#", Author = "Author B", PublishingYear = 2018, Publisher = "Publisher Y", copiesInStock = 15, copiesBorrowed = 0 }
        };

            var events = new List<Event>();
            foreach (var reader in readers)
            {
                foreach (var item in items)
                {
                    var borrowRecord = new BorrowRecord
                    {
                        BookId = item.Id,
                        DateBorrowed = DateTime.Now.AddDays(-45),
                    };
                    reader.BooksBorrowed.Add(borrowRecord);

                    var eventItem = new Event
                    {
                        UserId = reader.Id,
                        ItemId = item.Id,
                        Timestamp = borrowRecord.DateBorrowed,
                        Type = EventType.TakenOut
                    };
                    events.Add(eventItem);
                }
            }
            return (readers, items, events);
        }
    }

}
