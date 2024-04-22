using LibraryData.Models;

namespace LibraryData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public User GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            User existingUser = GetUser(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
            }
        }
        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }
    }
}
