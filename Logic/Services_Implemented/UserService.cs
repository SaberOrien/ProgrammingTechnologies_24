using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.DTOs_Implemented;
using Logic.Services_Abstract;

namespace Logic.Services_Implemented
{
    internal class UserService : IUserService
    {
        private IDataRepository _repository;
        public UserService(IDataRepository repository)
        {
            this._repository = repository;
        }

        private IUserDTO ToUserDTO(IUser user)
        {
            return new UserDTO(user.Id, user.Name, user.Surname, user.Email, user.UserType);
        }

        public async Task<IUserDTO> GetUser(int id)
        {
            return this.ToUserDTO(await this._repository.GetUser(id));
        }

        public async Task AddUser(int id, string name, string surname, string email, string userType)
        {
            await this._repository.AddUser(id, name, surname, email, userType);
        }
        public async Task DeleteUser(int id)
        {
            await this._repository.DeleteUser(id);
        }
        public async Task UpdateUser(int id, string name, string surname, string email, string userType)
        {
            await this._repository.UpdateUser(id, name, surname, email, userType);
        }
        public async Task<Dictionary<int, IUserDTO>> GetAllUsers()
        {
            Dictionary<int, IUserDTO> users = new Dictionary<int, IUserDTO>();
            foreach(IUser user in (await this._repository.GetAllUsers()).Values)
            {
                users.Add(user.Id, this.ToUserDTO(user));
            }
            return users;
        }
    }
}
