using MVVM.Model.Abstract;
using MVVM.View;
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
    public interface IItemViewModel
    {
        static IItemViewModel CreateViewModel(IItemFunctions function)
        {
            return new ItemViewModel(function);
        }

        ICommand CreateItem { get; set; }
        ICommand RemoveItem { get; set; }

        ObservableCollection<IItemDetailsViewModel> Items { get; set; }
        string Title { get; set; }
        int PublicationYear { get; set; }
        string Author { get; set; }
        object ItemType { get; set; }

        bool IsSelected { get; set; }
        Visibility ShowDetails { get; set; }
        IItemDetailsViewModel Details { get; set; }
    }
}
