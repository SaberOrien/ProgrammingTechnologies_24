using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public interface IUserDetailsViewModel
    {
        static IUserDetailsViewModel CreateViewModel(int id, string name, string surname, string email, string userType, IUserFunctions userFunctions)
        {
            return new UserDetailsViewModel(id, name, surname, email, userType, userFunctions);
        }

        ICommand UpdateUser {  get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
