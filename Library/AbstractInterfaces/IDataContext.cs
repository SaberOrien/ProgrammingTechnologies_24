using Data.ImplementedInterfaces;

namespace Data.AbstractInterfaces
{
    public interface IDataContext
    {
        static IDataContext CreateContext(string? connectionString = null)
        {
            return new DataContext(connectionString);
        }

        #region User
        Task<IUser?> GetUser(int id);
        Task AddUser(IUser user);
        Task DeleteUser(int id);
        Task UpdateUser(IUser user);
        Task<Dictionary<int, IUser>> GetAllUsers();
        #endregion User

        #region Item
        Task<IItem?> GetItem(int id);
        Task AddItem(IItem item);
        Task DeleteItem(int id);
        Task UpdateItem(IItem item);
        Task<Dictionary<int, IItem>> GetAllItems();
        #endregion Item

        #region Event
        Task<IEvent?> GetEvent(int id);
        Task AddEvent(IEvent @event);
        Task DeleteEvent(int id);
        Task UpdateEvent(IEvent @event);
        Task<Dictionary<int, IEvent>> GetAllEvents();
        #endregion

        #region State
        Task<IState?> GetState(int id);
        Task AddState(IState state);
        Task DeleteState(int id);
        Task UpdateState(IState state);
        Task<Dictionary<int, IState>> GetAllStates();
        #endregion

        Task<bool> CheckIfUserExists(int id);
        Task<bool> CheckIfItemExists(int id);
        Task<bool> CheckIfStateExists(int id);
        Task<bool> CheckIfEventExists(int id);
    }
}
