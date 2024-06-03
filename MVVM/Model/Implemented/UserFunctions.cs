using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.Implemented
{
    internal class UserFunctions : IUserFunctions
    {
        private IUserService _userService;

        public UserFunctions(IUserService userService)
        {
            _userService = userService ?? IUserService.CreateUserService();
        }

        private IUserModel toUserModel(IUserDTO userDTO)
        {
            return new UserModel(userDTO.Id, userDTO.Name, userDTO.Surname, userDTO.Email, userDTO.UserType);
        }

        public async Task<IUserModel> GetUser(int id)
        {
            return this.toUserModel(await this._userService.GetUser(id));
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
        public async Task<Dictionary<int, IUserModel>> GetAllUsers()
        {
            Dictionary<int, IUserModel> users = new Dictionary<int, IUserModel>();
            foreach(IUserDTO user in (await this._userService.GetAllUsers()).Values){
                users.Add(user.Id, this.toUserModel(user));
            }
            return users;
        }
    }
}
