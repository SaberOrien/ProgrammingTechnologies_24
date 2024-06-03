using MVVM.Model.Abstract;
using MVVM.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    internal class StateDetailsViewModel : IViewModel, IStateDetailsViewModel
    {
        public ICommand UpdateState {  get; set; }
        private readonly IStateFunctions _functions;
        private int _id; 
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private int _itemId;
        public int ItemId
        {
            get => _itemId;
            set
            {
                _itemId = value;
                OnPropertyChanged(nameof(ItemId));
            }
        }

        private int _itemAmount;
        public int ItemAmount
        {
            get => _itemAmount;
            set
            {
                _itemAmount = value;
                OnPropertyChanged(nameof(ItemAmount));
            }
        }

        public StateDetailsViewModel(IStateFunctions? stateFunctions = null)
        {
            this.UpdateState = new OnClickCommand(a => this.updateState(), c => this.canUpdateState());
            this._functions = IStateFunctions.CreateStateService();
        }

        public StateDetailsViewModel(int id, int itemId, int itemAmount, IStateFunctions? functions = null)
        {
            this.Id = id;
            this.ItemId = itemId;
            this.ItemAmount = itemAmount;

            this.UpdateState = new OnClickCommand(a => this.updateState(), c => this.canUpdateState());
            this._functions = IStateFunctions.CreateStateService();
        }

        private void updateState()
        {
            Task.Run(() =>
            {
                this._functions.UpdateState(this.Id, this.ItemId, this.ItemAmount);
            });
        }

        private bool canUpdateState()
        {
            return !(string.IsNullOrWhiteSpace(this.ItemId.ToString()) || string.IsNullOrWhiteSpace(this.ItemAmount.ToString()) || this.ItemAmount <= 0);
        }
    }
}
