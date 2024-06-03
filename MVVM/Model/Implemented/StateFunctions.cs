using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.Implemented
{
    internal class StateFunctions : IStateFunctions
    {
        private IStateService _stateService;
        public StateFunctions(IStateService? stateService = null)
        {
            _stateService = stateService ?? IStateService.CreateStateService();
        }

        private IStateModel toStateModel(IStateDTO state)
        {
            return new StateModel(state.Id, state.ItemId, state.ItemAmount);
        }
        public async Task<IStateModel> GetState(int id)
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
        public async Task<Dictionary<int, IStateModel>> GetAllStates()
        {
            Dictionary<int, IStateModel> states = new Dictionary<int, IStateModel>();
            foreach (IStateDTO state in (await this._stateService.GetAllStates()).Values)
            {
                states.Add(state.Id, this.toStateModel(state));
            }
            return states;
        }
    }
}
