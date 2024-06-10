using Logic.DTOs_Abstract;
using Logic.Services_Abstract;

namespace MVVM.Model
{
    public class UserFunctions : IUserFunctions
    {
        private IUserService _userService;

        public UserFunctions(IUserService userService)
        {
            _userService = userService ?? IUserService.CreateUserService();
        }

        private UserModel ToUserModel(IUserDTO userDTO)
        {
            return new UserModel(userDTO.Id, userDTO.Name, userDTO.Surname, userDTO.Email, userDTO.UserType);
        }

        public async Task<UserModel> GetUser(int id)
        {
            return this.ToUserModel(await this._userService.GetUser(id));
        }
        public async Task<Dictionary<int, UserModel>> GetUsers()
        {
            Dictionary<int, UserModel> users = new Dictionary<int, UserModel>();
            foreach (IUserDTO user in (await this._userService.GetUsers()).Values)
            {
                users.Add(user.Id, this.ToUserModel(user));
            }
            return users;
        }
        public async Task AddUser(int id, string name, string surname, string email, string userType)
        {
            await this._userService.AddUser(id, name, surname, email, userType);
        }
        public async Task DeleteUser(int id)
        {
            await this._userService.DeleteUser(id);
        }
        public async Task UpdateUser(int id, string name, string surname, string email, string userType)
        {
            await this._userService.UpdateUser(id, name, surname, email, userType);
        }
    }
}
