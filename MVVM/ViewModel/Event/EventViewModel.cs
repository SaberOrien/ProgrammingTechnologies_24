using MVVM.Model.Abstract;
using MVVM.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    internal class EventViewModel : IViewModel, IEventViewModel
    {
        public ICommand SwitchToUser { get; set; }
        public ICommand SwitchToItem { get; set; }
        public ICommand SwitchToState { get; set; }
        public ICommand BorrowEvent {  get; set; }
        public ICommand ReturnEvent { get; set; }
        public ICommand RemoveEvent { get; set; }

        private readonly IEventFunctions _eventFunctions;
        private ObservableCollection<IEventDetailsViewModel> _events;
        public ObservableCollection<IEventDetailsViewModel> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private int _stateId;
        public int StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
                OnPropertyChanged(nameof(StateId));
            }
        }
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        private bool _eventSelected;
        public bool EventSelected
        {
            get => _eventSelected;
            set
            {
                this.EventDetailsVisible = value ? Visibility.Visible : Visibility.Hidden;
                _eventSelected = value;
                OnPropertyChanged(nameof(EventSelected));
            }
        }
        private Visibility _eventDetailsVisible;
        public Visibility EventDetailsVisible
        {
            get => _eventDetailsVisible;
            set
            {
                _eventDetailsVisible = value;
                OnPropertyChanged(nameof(EventDetailsVisible));
            }
        }
        private IEventDetailsViewModel _eventDetailsViewModel;
        public IEventDetailsViewModel EventDetailsViewModel
        {
            get => _eventDetailsViewModel;
            set
            {
                _eventDetailsViewModel = value;
                this.EventSelected = true;
                OnPropertyChanged(nameof(EventDetailsViewModel));
            }
        }

        public EventViewModel(IEventFunctions? eventFunctions = null)
        {
            this.SwitchToUser = new SwitchCurrentViewCommand("UserView");
            this.SwitchToItem = new SwitchCurrentViewCommand("ItemView");
            this.SwitchToState = new SwitchCurrentViewCommand("StateView");

            this.BorrowEvent = new OnClickCommand(a => this.GetBorrowEvent(), c => CanGetEvent());
            this.ReturnEvent = new OnClickCommand(a => this.GetReturnEvent(), c => CanGetEvent());
            this.RemoveEvent = new OnClickCommand(a => this.DeleteEvent());

            this.Events = new ObservableCollection<IEventDetailsViewModel>();
            this._eventFunctions = eventFunctions ?? IEventFunctions.CreateEventFunctions();

            this.EventSelected = false;
            Task.Run(this.LoadEvents);
        }

        private async void LoadEvents()
        {
            Dictionary<int, IEventModel> Events = await this._eventFunctions.GetAllEvents();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._events.Clear();

                foreach(IEventModel @event in Events.Values)
                {
                    this._events.Add(new EventDetailsViewModel(@event.Id, @event.StateId, @event.UserId, @event.DateStamp, @event.EventType));
                }
            });
            OnPropertyChanged(nameof(Events));
        }
        private bool CanGetEvent()
        {
            return this.StateId > 0 && this.UserId > 0;
        }
        private void GetBorrowEvent()
        {
            Task.Run(async () =>
            {
                try
                {
                    var events = await this._eventFunctions.GetAllEvents();
                    int eventId = events.Count + 1;

                    await this._eventFunctions.AddEvent(eventId, this.StateId, this.UserId, "Borrow");
                    this.LoadEvents();
                } catch (Exception ex) { }
            });
        }
        private void GetReturnEvent()
        {
            Task.Run(async () =>
            {
                var events = await this._eventFunctions.GetAllEvents();
                int eventId = events.Count + 1;

                await this._eventFunctions.AddEvent(eventId, this.StateId, this.UserId, "Return");
                this.LoadEvents();
            });
        }
        private void DeleteEvent()
        {
            Task.Run(async () =>
            {
                await this._eventFunctions.DeleteEvent(this.EventDetailsViewModel.Id);
                this.LoadEvents();
            });
        }
    }
}
