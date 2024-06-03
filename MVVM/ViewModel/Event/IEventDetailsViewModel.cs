using MVVM.Model.Abstract;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public interface IEventDetailsViewModel
    {
        static IEventDetailsViewModel CreateViewModel(int id, int stateId, int userId, DateTime dateStamp, string eventType, IEventFunctions? eventFunctions = null)
        {
            return new EventDetailsViewModel(id, stateId, userId, dateStamp, eventType, eventFunctions);
        }
        ICommand UpdateEvent { get; set; }
        int Id { get; set; }
        int StateId { get; set; }
        int UserId { get; set; }
        DateTime DateStamp { get; set; }
        string EventType { get; set; }
    }
}
