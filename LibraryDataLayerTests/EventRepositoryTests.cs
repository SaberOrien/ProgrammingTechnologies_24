using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Repositories;
using LibraryData.Models;
using System.Linq;
using System.Collections.Generic;

namespace LibraryDataLayerTests
{
    [TestClass]
    public class EventRepositoryTests
    {
        private EventRepository _eventRepository;
        private ITestDataGenerator _dataGenerator;
        private List<Item> _items;
        private List<User> _users;

        [TestInitialize]
        public void TestInitialize()
        {
            // Toggle this line to switch between Randomized and Scripted TestDataGenerators
            bool useRandomizedData = true; // Set true for Randomized, false for Scripted

            _dataGenerator = useRandomizedData ? (ITestDataGenerator)new RandomizedTestDataGenerator()
                                               : new ScriptedTestDataGenerator();

            _items = _dataGenerator.GenerateItems(10);
            _users = _dataGenerator.GenerateUsers(5);
            var events = _dataGenerator.GenerateEvents(10, _items, _users);

            _eventRepository = new EventRepository();
            foreach (var evt in events)
            {
                _eventRepository.AddEvent(evt);
            }
        }

        [TestMethod]
        public void AddEvent_IncreasesEventCount()
        {
            var initialCount = _eventRepository.GetEventsByItem(_items[0].Id).Count();
            var newEvent = new TakenOut(_items[0].Id, System.DateTime.Now, _users[0].Id);
            _eventRepository.AddEvent(newEvent);
            var newCount = _eventRepository.GetEventsByItem(_items[0].Id).Count();

            Assert.AreEqual(initialCount + 1, newCount);
        }

        [TestMethod]
        public void GetEventsByItem_ReturnsCorrectEvents()
        {
            var events = _eventRepository.GetEventsByItem(_items[1].Id);
            Assert.IsTrue(events.All(e => e.ItemId == _items[1].Id));
        }

        [TestMethod]
        public void GetEventsByUser_ReturnsCorrectEvents()
        {
            var events = _eventRepository.GetEventsByUser(_users[1].Id);
            Assert.IsTrue(events.Any(e => (e is TakenOut to && to.UserId == _users[1].Id) ||
                                          (e is Returned r && r.UserId == _users[1].Id)));
        }
    }
}
