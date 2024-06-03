using Data.AbstractInterfaces;
using Logic.Services_Implemented;
using Logic.DTOs_Abstract;

namespace Logic.Services_Abstract
{
    public interface IUserService
    {
        static IUserService CreateUserService(IDataRepository? dataRepository = null)
        {
            return new UserService(dataRepository ?? IDataRepository.CreateDatabase());
        }
        Task<IUserDTO> GetUser(int id);
        Task AddUser(int id, string name, string surname, string email, string userType);
        Task DeleteUser(int id);
        Task UpdateUser(int id, string name, string surname, string email, string userType);
        Task<Dictionary<int, IUserDTO>> GetAllUsers();
    }
}
