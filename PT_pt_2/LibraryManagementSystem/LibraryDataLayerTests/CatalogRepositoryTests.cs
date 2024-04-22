using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Repositories;
using LibraryData.Models;
using System.Linq;

namespace LibraryDataLayerTests
{
    [TestClass]
    public class CatalogRepositoryTests
    {
        private CatalogRepository _catalogRepository;
        private ITestDataGenerator _dataGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            bool useRandomizedData = true;

            _dataGenerator = useRandomizedData ? (ITestDataGenerator)new RandomizedTestDataGenerator()
                                               : new ScriptedTestDataGenerator();

            var items = _dataGenerator.GenerateItems(10);
            _catalogRepository = new CatalogRepository();
            foreach (var item in items)
            {
                _catalogRepository.AddItem(item);
            }
        }

        [TestMethod]
        public void GetItem_ExistingId_ReturnsCorrectItem()
        {
            var expectedItem = _catalogRepository.GetItem(1);
            Assert.IsNotNull(expectedItem);
            Assert.IsTrue(expectedItem.Title.Contains("Title 1"), "The item title should include 'Title 1'");
        }

        [TestMethod]
        public void GetAllItems_ReturnsAllItems()
        {
            var items = _catalogRepository.GetAllItems();
            Assert.AreEqual(10, items.Count());
        }

        [TestMethod]
        public void UpdateItem_ChangesItemDetails()
        {
            var originalItem = _catalogRepository.GetItem(2);
            originalItem.Title = "Updated Title";
            _catalogRepository.UpdateItem(originalItem);

            var updatedItem = _catalogRepository.GetItem(2);
            Assert.AreEqual("Updated Title", updatedItem.Title);
        }
    }
}
