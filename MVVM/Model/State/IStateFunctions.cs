using Logic.DTOs_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    public interface IStateFunctions
    {
        Task<StateModel> GetState(int id);
        Task AddState(int id, int itemId, int itemAmount);
        Task DeleteState(int id);
        Task UpdateState(int id, int itemId, int itemAmount);
        Task<Dictionary<int, StateModel>> GetAllStates();

    }
}
