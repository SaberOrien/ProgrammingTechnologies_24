using MVVM.Model;
using MVVM.ViewModel.Commands;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class StateDetailsViewModel : IViewModel
    {
        public ICommand UpdateState {  get; set; }
        private readonly StateFunctions _functions;
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

        public StateDetailsViewModel(StateFunctions? stateFunctions = null)
        {
            this.UpdateState = new OnClickCommand(a => this.updateState(), c => this.canUpdateState());
            this._functions = stateFunctions ?? new StateFunctions(null);
        }

        public StateDetailsViewModel(int id, int itemId, int itemAmount, StateFunctions? functions = null)
        {
            this.Id = id;
            this.ItemId = itemId;
            this.ItemAmount = itemAmount;

            this.UpdateState = new OnClickCommand(a => this.updateState(), c => this.canUpdateState());
            this._functions = functions ?? new StateFunctions(null);
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
