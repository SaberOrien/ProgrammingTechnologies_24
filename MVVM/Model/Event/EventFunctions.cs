using Logic.Services_Abstract;
using Logic.DTOs_Abstract;

namespace MVVM.Model
{
    public class EventFunctions : IEventFunctions
    {
        private IEventService _eventService;
        public EventFunctions(IEventService eventService)
        {
            this._eventService = eventService ?? IEventService.CreateEventService();
        }

        private EventModel toEventModel(IEventDTO eventDTO)
        {
            return new EventModel(eventDTO.Id, eventDTO.StateId, eventDTO.UserId, eventDTO.DateStamp, eventDTO.EventType);
        }

        public async Task<EventModel> GetEvent(int id)
        {
            return this.toEventModel(await this._eventService.GetEvent(id));
        } 
        public async Task AddEvent(int id, int stateId, int userId, string type)
        {
            await this._eventService.AddEvent(id, stateId, userId, type);
        }
        public async Task DeleteEvent(int id)
        {
            await this._eventService.DeleteEvent(id);
        }
        public async Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string type)
        {
            await this._eventService.UpdateEvent(id, stateId, userId, dateStamp, type);
        }
        public async Task<Dictionary<int, EventModel>> GetAllEvents()
        {
            Dictionary<int, EventModel> events = new Dictionary<int, EventModel>();
            foreach (IEventDTO eventDTO in (await this._eventService.GetEvents()).Values)
            {
                events.Add(eventDTO.Id, toEventModel(eventDTO));
            }
            return events;
        }
    }
}
