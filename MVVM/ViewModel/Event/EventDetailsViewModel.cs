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
    internal class EventDetailsViewModel : IViewModel, IEventDetailsViewModel
    {
        public ICommand UpdateEvent { get; set; }
        private readonly IEventFunctions _eventFunctions;

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
    
        public EventDetailsViewModel(IEventFunctions? eventFunctions = null)
        {
            this.UpdateEvent = new OnClickCommand(a => this.updateEvent(), c => this.canUpdateEvent());
            this._eventFunctions = eventFunctions ?? IEventFunctions.CreateEventFunctions();
        }
        
        public EventDetailsViewModel(int id, int stateId, int userId, DateTime dateStamp, string eventType, IEventFunctions? eventFunctions = null)
        {
            this.UpdateEvent = new OnClickCommand(a => this.updateEvent(), c => this.canUpdateEvent());
            this._eventFunctions = eventFunctions ?? IEventFunctions.CreateEventFunctions();

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
