using MVVM.Model.Abstract;
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
    public interface IEventViewModel
    {
        static IEventViewModel CreateViewModel(IEventFunctions function)
        {
            return new EventViewModel(function);
        }
        ICommand BorrowEvent {  get; set; }
        ICommand ReturnEvent { get; set; }
        ICommand RemoveEvent { get; set; }

        ObservableCollection<IEventDetailsViewModel> Events { get; set; }

        int StateId { get; set; }
        int UserId { get; set; }

        bool EventSelected { get; set; }
        Visibility EventDetailsVisible {  get; set; }
        IEventDetailsViewModel EventDetailsViewModel { get; set; }
    }
}
