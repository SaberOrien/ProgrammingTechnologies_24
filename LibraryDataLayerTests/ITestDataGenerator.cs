using LibraryData.Models;

namespace LibraryDataLayerTests
{
    internal interface ITestDataGenerator
    {
        List<Item> GenerateItems(int count);
        List<User> GenerateUsers(int count);
        List<Event> GenerateEvents(int count, List<Item> items, List<User> users);
    }
}
