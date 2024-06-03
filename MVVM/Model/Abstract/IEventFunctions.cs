using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using MVVM.Model.Implemented;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.Abstract
{
    public interface IEventFunctions
    {
        static IEventFunctions CreateEventFunctions(IEventService? eventService = null)
        {
            return new EventFunctions(eventService ?? IEventService.CreateEventService());
        }
        Task<IEventModel> GetEvent(int id);
        Task AddEvent(int id, int stateId, int userId, string type);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string type);
        Task<Dictionary<int, IEventModel>> GetAllEvents();
    }
}
