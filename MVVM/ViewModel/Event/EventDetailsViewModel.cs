using MVVM.Model;
using MVVM.ViewModel.Commands;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class EventDetailsViewModel : IViewModel
    {
        public ICommand UpdateEvent { get; set; }
        private readonly EventFunctions _eventFunctions;

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
        private DateTime _dateStamp;
        public DateTime DateStamp
        {
            get => _dateStamp;
            set
            {
                _dateStamp = value;
                OnPropertyChanged(nameof(DateStamp));
            }
        }
        private string _eventType;
        public string EventType
        {
            get => _eventType;
            set
            {
                _eventType = value;
                OnPropertyChanged(nameof(EventType));
            }
        }
    
        public EventDetailsViewModel(EventFunctions? eventFunctions = null)
        {
            this.UpdateEvent = new OnClickCommand(a => this.updateEvent(), c => this.canUpdateEvent());
            this._eventFunctions = eventFunctions ?? new EventFunctions(null);// ?? IEventFunctions.CreateEventFunctions();
        }
        
        public EventDetailsViewModel(int id, int stateId, int userId, DateTime dateStamp, string eventType, EventFunctions? eventFunctions = null)
        {
            this.UpdateEvent = new OnClickCommand(a => this.updateEvent(), c => this.canUpdateEvent());
            this._eventFunctions = eventFunctions ?? new EventFunctions(null);// ?? IEventFunctions.CreateEventFunctions();

            Id = id;
            StateId = stateId;
            UserId = userId;
            DateStamp = dateStamp;
            EventType = eventType;

        }

        private void updateEvent()
        {
            Task.Run(async () =>
            {
                await this._eventFunctions.UpdateEvent(this.Id, this.StateId, this.UserId, this.DateStamp, this.EventType);
            });
        }

        private bool canUpdateEvent()
        {
            return !(string.IsNullOrWhiteSpace(this.DateStamp.ToString()) || string.IsNullOrWhiteSpace(this.EventType));
        }
    }

}
