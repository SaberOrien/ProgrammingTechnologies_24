using MVVM.Model;
using MVVM.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class UserViewModel : IViewModel
    {
        public ICommand SwitchToEvent { get; set; }
        public ICommand SwitchToItem { get; set; }
        public ICommand SwitchToState { get; set; }
        public ICommand CreateUser { get; set; }
        public ICommand RemoveUser { get; set; }


        private readonly UserFunctions userFunctions;
        private ObservableCollection<UserDetailsViewModel> _privUsers;

        public ObservableCollection<UserDetailsViewModel> Users
        {
            get => _privUsers;
            set
            {
                _privUsers = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserViewModel(UserFunctions? userFunctions = null)
        {
            this.SwitchToItem = new SwitchCurrentViewCommand("ItemView");
            this.SwitchToState = new SwitchCurrentViewCommand("StateView");
            this.SwitchToEvent = new SwitchCurrentViewCommand("EventView");
            this.CreateUser = new OnClickCommand(a => this.GetUser(), c => this.CanGetUser());
            this.RemoveUser = new OnClickCommand(a => this.DeleteUser());

            this.Users = new ObservableCollection<UserDetailsViewModel>();

            this.userFunctions = userFunctions ?? new UserFunctions(null);//IUserFunctions.CreateUserFunctions();
            this.IsSelected = false;

            Task.Run(this.LoadUsers);
        }

        public void GetUser()
        {
            Task.Run(async () =>
            {
                var tempUsers = await this.userFunctions.GetUsers();
                int userId = tempUsers.Count + 1;
                await this.userFunctions.AddUser(userId, this.Name, this.Surname, this.Email, this.UserType.ToString());
                this.LoadUsers();
            });
        }

        public bool CanGetUser()
        {
            return !(string.IsNullOrWhiteSpace(this.Name) || string.IsNullOrWhiteSpace(this.Surname) || string.IsNullOrWhiteSpace(this.Email) || this.UserType == null || string.IsNullOrWhiteSpace(this.UserType.ToString()));
        }

        public async void LoadUsers()
        {
            Dictionary<int, UserModel> users = await this.userFunctions.GetUsers();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._privUsers.Clear();
                foreach (UserModel user in users.Values)
                {
                    this._privUsers.Add(new UserDetailsViewModel(user.Id, user.Name, user.Surname, user.Email, user.UserType));
                }
            });
            OnPropertyChanged(nameof(users));
        }

        public void DeleteUser()
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

        private UserDetailsViewModel _selectedUserViewModel;
        public UserDetailsViewModel SelectedUserViewModel
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
