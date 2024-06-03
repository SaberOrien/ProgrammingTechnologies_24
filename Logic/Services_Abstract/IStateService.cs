using Data.AbstractInterfaces;
using Logic.Services_Implemented;
using Logic.DTOs_Abstract;

namespace Logic.Services_Abstract
{
    public interface IStateService
    {
        static IStateService CreateStateService(IDataRepository? dataRepository = null)
        {
            return new StateService(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task<IStateDTO> GetState(int id);
        Task AddState(int id, int itemId, int itemAmount);
        Task DeleteState(int id);
        Task UpdateState(int id, int itemId, int itemAmount);
        Task<Dictionary<int, IStateDTO>> GetAllStates();
    }
}
