using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.DTOs_Implemented;
using Logic.Services_Abstract;

namespace Logic.Services_Implemented
{
    internal class EventService : IEventService
    {
        private IDataRepository _repository;
        public EventService(IDataRepository repository)
        {
            _repository = repository;
        }

        private IEventDTO ToEventDTO(IEvent @event)
        {
            return new EventDTO(@event.Id, @event.StateId, @event.UserId, @event.DateStamp, @event.EventType);
        }
        public async Task<IEventDTO> GetEvent(int id)
        {
            return this.ToEventDTO(await this._repository.GetEvent(id));
        }
        public async Task<Dictionary<int, IEventDTO>> GetEvents()
        {
            Dictionary<int, IEventDTO> events = new Dictionary<int, IEventDTO>();

            foreach (IEvent @event in (await this._repository.GetEvents()).Values)
            {
                events.Add(@event.Id, ToEventDTO(@event));
            }
            return events;
        }
        public async Task AddEvent(int id, int stateId, int userId, string type)
        {
            await this._repository.AddEvent(id, stateId, userId, type);
        }
        public async Task DeleteEvent(int id)
        {
            await this._repository.DeleteEvent(id);
        }
        public async Task UpdateEvent(int id, int stateId,int userId, DateTime dateStamp, string type)
        {
            await this._repository.UpdateEvent(id, stateId, userId, dateStamp, type);
        }
    }
}
