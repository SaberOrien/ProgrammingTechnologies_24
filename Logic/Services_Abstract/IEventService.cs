using Data.AbstractInterfaces;
using Logic.Services_Implemented;
using Logic.DTOs_Abstract;

namespace Logic.Services_Abstract
{
    public interface IEventService
    {
        static IEventService CreateEventService(IDataRepository? dataRepository = null)
        {
            return new EventService(dataRepository ?? IDataRepository.CreateDatabase());
        }
        Task<IEventDTO> GetEvent(int id);
        Task AddEvent(int id, int stateId, int userId, string type);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string type);
        Task<Dictionary<int, IEventDTO>> GetAllEvents();
    }
}
