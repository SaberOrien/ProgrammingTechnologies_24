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
    public interface IUserViewModel
    {
        static IUserViewModel CreateViewModel(IUserFunctions function)
        {
            return new UserViewModel(function);
        }

        ICommand CreateUser { get; set; }
        ICommand RemoveUser { get; set; }

        ObservableCollection<IUserDetailsViewModel> Users { get; set; }

        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        object UserType { get; set; }
        bool IsSelected {  get; set; }

        Visibility ShowUserDetails { get; set; }
        IUserDetailsViewModel SelectedUserViewModel { get; set; }
    }
}
