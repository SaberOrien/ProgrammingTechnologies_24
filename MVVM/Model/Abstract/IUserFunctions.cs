using Logic.Services_Abstract;
using MVVM.Model.Implemented;

namespace MVVM.Model.Abstract
{
    public interface IUserFunctions
    {
        static IUserFunctions CreateModelOperation(IUserService? userService = null)
        {
            return new UserFunctions(userService);
        }
        Task<IUserModel> GetUser(int id);
        Task AddUser(int id, string name, string surname, string email, string userType);
        Task DeleteUser(int id);
        Task UpdateUser(int id, string name, string surname, string email, string userType);
        Task<Dictionary<int, IUserModel>> GetAllUsers();
    }
}
