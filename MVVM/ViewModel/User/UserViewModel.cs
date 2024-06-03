using MVVM.Model.Abstract;
using MVVM.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    internal class UserViewModel : IViewModel, IUserViewModel
    {
        public ICommand SwitchToEvent { get; set; }
        public ICommand SwitchToItem { get; set; }
        public ICommand SwitchToState { get; set; }
        public ICommand CreateUser { get; set; }
        public ICommand RemoveUser { get; set; }


        private readonly IUserFunctions userFunctions;
        private ObservableCollection<IUserDetailsViewModel> _privUsers;

        public ObservableCollection<IUserDetailsViewModel> Users
        {
            get => _privUsers;
            set
            {
                _privUsers = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserViewModel(IUserFunctions? userFunctions = null)
        {
            this.SwitchToItem = new SwitchCurrentViewCommand("ItemView");
            this.SwitchToState = new SwitchCurrentViewCommand("StateView");
            this.SwitchToEvent = new SwitchCurrentViewCommand("EventView");
            this.CreateUser = new OnClickCommand(a => this.GetUser(), c => this.CanGetUser());
            this.RemoveUser = new OnClickCommand(a => this.DeleteUser());

            this.Users = new ObservableCollection<IUserDetailsViewModel>();

            this.userFunctions = userFunctions ?? IUserFunctions.CreateModelOperation();
            this.IsSelected = false;

            Task.Run(this.LoadUsers);
        }

        private void GetUser()
        {
            Task.Run(async () =>
            {
                var tempUsers = await this.userFunctions.GetAllUsers();
                int userId = tempUsers.Count + 1;
                await this.userFunctions.AddUser(userId, this.Name, this.Surname, this.Email, this.UserType.ToString());
                this.LoadUsers();
            });
        }

        private bool CanGetUser()
        {
            return !(string.IsNullOrWhiteSpace(this.Name) || string.IsNullOrWhiteSpace(this.Surname) || string.IsNullOrWhiteSpace(this.Email) || this.UserType == null || string.IsNullOrWhiteSpace(this.UserType.ToString()));
        }

        private async void LoadUsers()
        {
            Dictionary<int, IUserModel> users = await this.userFunctions.GetAllUsers();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._privUsers.Clear();
                foreach (IUserModel user in users.Values)
                {
                    this._privUsers.Add(new UserDetailsViewModel(user.Id, user.Name, user.Surname, user.Email, user.UserType));
                }
            });
            OnPropertyChanged(nameof(users));
        }

        private void DeleteUser()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this.userFunctions.DeleteUser(this.SelectedUserViewModel.Id);
                    this.LoadUsers();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            });
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private object _userType;
        public object UserType
        {
            get => _userType;
            set
            {
                _userType = value is ComboBoxItem item ? item.Content.ToString() : value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                this.ShowUserDetails = value ? Visibility.Visible : Visibility.Hidden;
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private Visibility _showUserDetails;
        public Visibility ShowUserDetails
        {
            get => _showUserDetails;
            set
            {
                _showUserDetails = value;
                OnPropertyChanged(nameof(ShowUserDetails));
            }
        }

        private IUserDetailsViewModel _selectedUserViewModel;
        public IUserDetailsViewModel SelectedUserViewModel
        {
            get => _selectedUserViewModel;
            set
            {
                _selectedUserViewModel = value;
                this.IsSelected = true;
                OnPropertyChanged(nameof(SelectedUserViewModel));
            }
        }
    }
}
