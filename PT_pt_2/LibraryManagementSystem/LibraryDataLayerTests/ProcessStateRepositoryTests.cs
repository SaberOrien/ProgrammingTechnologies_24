using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Repositories;
using LibraryData.Models;
using System.Linq;

namespace LibraryDataLayerTests
{
    [TestClass]
    public class ProcessStateRepositoryTests
    {
        private ProcessStateRepository _processStateRepository;
        private ITestDataGenerator _dataGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            // Toggle this line to switch between Randomized and Scripted TestDataGenerators
            bool useRandomizedData = false; // Set true for Randomized, false for Scripted

            _dataGenerator = useRandomizedData ? (ITestDataGenerator)new RandomizedTestDataGenerator()
                                               : new ScriptedTestDataGenerator();

            var items = _dataGenerator.GenerateItems(20);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].IsAvailable = i % 2 == 0;
            }

            _processStateRepository = new ProcessStateRepository(items);
        }

        [TestMethod]
        public void GetAvailableItems_ReturnsOnlyAvailableItems()
        {
            var availableItems = _processStateRepository.GetAvailableItems();
            Assert.AreEqual(10, availableItems.Count());
            Assert.IsTrue(availableItems.All(item => item.IsAvailable));
        }

        [TestMethod]
        public void GetCheckedOutItems_ReturnsOnlyCheckedOutItems()
        {
            var checkedOutItems = _processStateRepository.GetCheckedOutItems();
            Assert.AreEqual(10, checkedOutItems.Count());
            Assert.IsTrue(checkedOutItems.All(item => !item.IsAvailable));
        }
    }
}
