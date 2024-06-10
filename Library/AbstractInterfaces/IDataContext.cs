using Data.ImplementedInterfaces;

namespace Data.AbstractInterfaces
{
    public interface IDataContext
    {
        static IDataContext CreateContext(string? connectionString = null)
        {
            return new DataContext(connectionString);
        }

        Task<IUser?> GetUser(int id);
        Task<Dictionary<int, IUser>> GetUsers();
        Task AddUser(IUser user);
        Task DeleteUser(int id);
        Task UpdateUser(IUser user);

        Task<IItem?> GetItem(int id);
        Task<Dictionary<int, IItem>> GetItems();
        Task AddItem(IItem item);
        Task DeleteItem(int id);
        Task UpdateItem(IItem item);

        #region Event
        Task<IEvent?> GetEvent(int id);
        Task AddEvent(IEvent @event);
        Task DeleteEvent(int id);
        Task UpdateEvent(IEvent @event);
        Task<Dictionary<int, IEvent>> GetEvents();
        #endregion

        #region State
        Task<IState?> GetState(int id);
        Task AddState(IState state);
        Task DeleteState(int id);
        Task UpdateState(IState state);
        Task<Dictionary<int, IState>> GetStates();
        #endregion

        Task<bool> CheckIfUserExists(int id);
        Task<bool> CheckIfItemExists(int id);
        Task<bool> CheckIfStateExists(int id);
        Task<bool> CheckIfEventExists(int id);
    }
}
