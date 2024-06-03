using MVVM.Model.Abstract;
using MVVM.ViewModel.Commands;
using System;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    internal class UserDetailsViewModel : IViewModel, IUserDetailsViewModel
    {
        public ICommand UpdateUser { get; set; }

        private readonly IUserFunctions _userFunctions;

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

        private string _userType;
        public string UserType
        {
            get => _userType;
            set
            {
                _userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public UserDetailsViewModel(IUserFunctions? userFunctions = null)
        {
            this.UpdateUser = new OnClickCommand(a => this.updateUser(), c => this.checkIfCanUpdate());
            this._userFunctions = userFunctions ?? IUserFunctions.CreateModelOperation();
        }

        public UserDetailsViewModel(int id, string name, string surname, string email, string userType, IUserFunctions? userFunctions = null)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.UserType = userType;

            this.UpdateUser = new OnClickCommand(a => this.updateUser(), c => this.checkIfCanUpdate());
            this._userFunctions = userFunctions ?? IUserFunctions.CreateModelOperation();
        }

        private void updateUser()
        {
            Task.Run(() =>
            {
                this._userFunctions.UpdateUser(this.Id, this.Name, this.Surname, this.Email, this.UserType);
            });
        }

        private bool checkIfCanUpdate()
        {
            return !(string.IsNullOrWhiteSpace(this.Name) || string.IsNullOrWhiteSpace(this.Surname) || string.IsNullOrWhiteSpace(this.Email) || string.IsNullOrWhiteSpace(this.UserType));
        }
    }
}
