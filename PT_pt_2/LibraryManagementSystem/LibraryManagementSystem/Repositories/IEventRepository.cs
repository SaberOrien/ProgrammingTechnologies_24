using LibraryData.Models;

namespace LibraryData.Repositories
{
    public interface IEventRepository
    {
        void AddEvent(Event newEvent);
        IEnumerable<Event> GetEventsByItem(int itemId);
        IEnumerable<Event> GetEventsByUser(int userId);
    }
}

