using MVVM.Model;
using MVVM.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class StateViewModel : IViewModel
    {
        public ICommand SwitchToUser { get; set; }
        public ICommand SwitchToEvent { get; set; }
        public ICommand SwitchToItem { get; set; }

        public ICommand CreateState {  get; set; }
        public ICommand RemoveState { get; set; }

        private readonly StateFunctions _stateFunctions;
        private ObservableCollection<StateDetailsViewModel> _stateDetails;
        public ObservableCollection<StateDetailsViewModel> StateDetails
        {
            get => _stateDetails;
            set
            {
                _stateDetails = value;
                OnPropertyChanged(nameof(StateDetails));
            }
        }

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

        private bool _stateSelected;
        public bool StateSelected
        {
            get => _stateSelected;
            set
            {
                this.StateDetailsVisible = value ? Visibility.Visible : Visibility.Hidden;
                _stateSelected = value;
                OnPropertyChanged(nameof(StateSelected));
            }
        }

        private Visibility _stateDetailsVisible;
        public Visibility StateDetailsVisible
        {
            get => _stateDetailsVisible;
            set
            {
                _stateDetailsVisible = value;
                OnPropertyChanged(nameof(StateDetailsVisible));
            }
        }

        private StateDetailsViewModel _stateDetailsViewModel;
        public StateDetailsViewModel StateDetailsViewModel
        {
            get => _stateDetailsViewModel;
            set
            {
                _stateDetailsViewModel = value;
                this.StateSelected = true;
                OnPropertyChanged(nameof(StateDetailsViewModel));
            }
        }
    
        public StateViewModel(StateFunctions? stateFunctions = null)
        {
            this.SwitchToUser = new SwitchCurrentViewCommand("UserView");
            this.SwitchToItem = new SwitchCurrentViewCommand("ItemView");
            this.SwitchToEvent = new SwitchCurrentViewCommand("EventView");

            this.CreateState = new OnClickCommand(a => this.GetState(), c => this.CanGetState());
            this.RemoveState = new OnClickCommand(a => this.DeleteState());

            this.StateDetails = new ObservableCollection<StateDetailsViewModel>();

            this._stateFunctions = stateFunctions ?? new StateFunctions(null);
            this.StateSelected = false;

            Task.Run(this.LoadStates);
        }

        private async void LoadStates()
        {
            Dictionary<int, StateModel> States = await this._stateFunctions.GetAllStates();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._stateDetails.Clear();

                foreach(var state in States.Values)
                {
                    this._stateDetails.Add(new StateDetailsViewModel(state.Id, state.ItemId, state.ItemAmount));
                }
            });

            OnPropertyChanged(nameof(States));
        }
    
        private void GetState()
        {
            Task.Run(async () =>
            {
                try
                {
                    var states = await this._stateFunctions.GetAllStates();
                    int stateId = states.Count + 1;

                    await this._stateFunctions.AddState(stateId, this.ItemId, this.ItemAmount);
                    this.LoadStates();
                } catch(Exception ex) { }
            });
        }
    
        private bool CanGetState()
        {
            return !(string.IsNullOrWhiteSpace(this.ItemId.ToString()) || string.IsNullOrWhiteSpace(this.ItemAmount.ToString()) || this.ItemId <= 0 || this.ItemAmount <= 0);
        }

        private void DeleteState()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._stateFunctions.DeleteState(this.StateDetailsViewModel.Id);
                    this.LoadStates();
                } catch(Exception ex) { }
            });
        }
    }
}
