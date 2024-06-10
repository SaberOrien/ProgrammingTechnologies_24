using Logic.DTOs_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    public interface IEventFunctions
    {
        Task<EventModel> GetEvent(int id);
        Task AddEvent(int id, int stateId, int userId, string type);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string type);
        Task<Dictionary<int, EventModel>> GetAllEvents();
    }
}
