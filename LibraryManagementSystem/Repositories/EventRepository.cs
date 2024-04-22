using LibraryData.Models;

namespace LibraryData.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly List<Event> _events;

        public EventRepository()
        {
            _events = new List<Event>();
        }

        public void AddEvent(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public IEnumerable<Event> GetEventsByItem(int itemId)
        {
            return _events.Where(e => e.ItemId == itemId).ToList();
        }

        public IEnumerable<Event> GetEventsByUser(int userId)
        {
            return _events.Where(e => (e is TakenOut && ((TakenOut)e).UserId == userId) ||
                                      (e is Returned && ((Returned)e).UserId == userId)).ToList();
        }
    }
}

