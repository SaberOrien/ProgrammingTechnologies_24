using Logic.Services_Abstract;
using Logic.DTOs_Abstract;
using MVVM.Model.Abstract;

namespace MVVM.Model.Implemented
{
    internal class EventFunctions : IEventFunctions
    {
        private IEventService _eventService;
        public EventFunctions(IEventService eventService)
        {
            this._eventService = eventService ?? IEventService.CreateEventService();
        }

        private IEventModel toEventModel(IEventDTO eventDTO)
        {
            return new EventModel(eventDTO.Id, eventDTO.StateId, eventDTO.UserId, eventDTO.DateStamp, eventDTO.EventType);
        }

        public async Task<IEventModel> GetEvent(int id)
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
        public async Task<Dictionary<int, IEventModel>> GetAllEvents()
        {
            Dictionary<int, IEventModel> events = new Dictionary<int, IEventModel>();
            foreach (IEventDTO eventDTO in (await this._eventService.GetAllEvents()).Values)
            {
                events.Add(eventDTO.Id, toEventModel(eventDTO));
            }
            return events;
        }
    }
}
