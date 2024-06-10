using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    public interface IUserFunctions
    {
        Task<UserModel> GetUser(int id);
        Task<Dictionary<int, UserModel>> GetUsers();
        Task AddUser(int id, string name, string surname, string email, string userType);
        Task DeleteUser(int id);
        Task UpdateUser(int id, string name, string surname, string email, string userType);
    }
}
