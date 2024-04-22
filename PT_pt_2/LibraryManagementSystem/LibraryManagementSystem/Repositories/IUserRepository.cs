using LibraryData.Models;

namespace LibraryData.Repositories
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
