using Logic.Services_Abstract;
using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.DTOs_Implemented;
using System.Reflection;

namespace Logic.Services_Implemented
{
    internal class StateService : IStateService
    {
        private IDataRepository _repository;

        public StateService(IDataRepository repository)
        {
            _repository = repository;
        }

        private IStateDTO toStateDTO(IState state)
        {
            return new StateDTO(state.Id, state.ItemId, state.ItemAmount);
        }

        public async Task<IStateDTO> GetState(int id)
        {
            return this.toStateDTO(await this._repository.GetState(id));
        }
        public async Task AddState(int id, int itemId, int itemAmount)
        {
            await _repository.AddState(id, itemId, itemAmount);
        }
        public async Task DeleteState(int id)
        {
            await this._repository.DeleteState(id);
        }
        public async Task UpdateState(int id, int itemId, int itemAmount)
        {
            await this._repository.UpdateState(id, itemId, itemAmount);
        }
        public async Task<Dictionary<int, IStateDTO>> GetAllStates()
        {
            Dictionary<int, IStateDTO> states = new Dictionary<int, IStateDTO>();
            foreach (IState state in (await this._repository.GetAllStates()).Values)
            {
                states.Add(state.Id, this.toStateDTO(state));
            }
            return states;
        }
    }
}
