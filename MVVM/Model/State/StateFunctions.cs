using Logic.DTOs_Abstract;
using Logic.Services_Abstract;

namespace MVVM.Model
{
    public class StateFunctions
    {
        private IStateService _stateService;
        public StateFunctions(IStateService? stateService = null)
        {
            _stateService = stateService ?? IStateService.CreateStateService();
        }

        private StateModel toStateModel(IStateDTO state)
        {
            return new StateModel(state.Id, state.ItemId, state.ItemAmount);
        }
        public async Task<StateModel> GetState(int id)
        {
            return this.toStateModel(await this._stateService.GetState(id));
        }
        public async Task AddState(int id, int itemId, int itemAmount)
        {
            await this._stateService.AddState(id, itemId, itemAmount);
        }
        public async Task DeleteState(int id)
        {
            await this._stateService.DeleteState(id);
        }
        public async Task UpdateState(int id, int itemId, int itemAmount)
        {
            await this._stateService.UpdateState(id, itemId, itemAmount);
        }
        public async Task<Dictionary<int, StateModel>> GetAllStates()
        {
            Dictionary<int, StateModel> states = new Dictionary<int, StateModel>();
            foreach (IStateDTO state in (await this._stateService.GetStates()).Values)
            {
                states.Add(state.Id, this.toStateModel(state));
            }
            return states;
        }
    }
}
