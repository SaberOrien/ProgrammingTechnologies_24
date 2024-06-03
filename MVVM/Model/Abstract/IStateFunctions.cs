using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using MVVM.Model.Implemented;
using Logic.Services_Abstract;

namespace MVVM.Model.Abstract
{
    public interface IStateFunctions
    {
        static IStateFunctions CreateStateService(IStateService? stateService = null)
        {
            return new StateFunctions(stateService ?? IStateService.CreateStateService());
        }
        Task<IStateModel> GetState(int id);
        Task AddState(int id, int itemId, int itemAmount);
        Task DeleteState(int id);
        Task UpdateState(int id, int itemId, int itemAmount);
        Task<Dictionary<int, IStateModel>> GetAllStates();
    }
}
