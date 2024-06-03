using Data.ImplementedInterfaces;

namespace Data.AbstractInterfaces
{
    public interface IDataRepository
    {
        static IDataRepository CreateDatabase(IDataContext? dataContext = null)
        {
            return new DataRepository(dataContext ?? new DataContext());
        }
        #region User
        Task<IUser> GetUser(int id);
        Task AddUser(int id, string name, string surname, string email, string userType);
        Task DeleteUser(int id);
        Task UpdateUser(int id, string name, string surname, string email, string userType);
        Task<Dictionary<int, IUser>> GetAllUsers();
        #endregion

        #region Item
        Task<IItem> GetItem(int id);
        Task AddItem(int id, string title, int publicationYear, string author, string itemType);
        Task DeleteItem(int id);
        Task UpdateItem(int id, string title, int publicationYear, string author, string itemType);
        Task<Dictionary<int, IItem>> GetAllItems();
        #endregion

        #region State
        Task<IState> GetState(int id);
        Task AddState(int id, int itemId, int itemAmount);
        Task DeleteState(int id);
        Task UpdateState(int id, int itemId, int itemAmount);
        Task<Dictionary<int, IState>> GetAllStates();
        #endregion

        #region Event
        Task<IEvent> GetEvent(int id);
        Task AddEvent(int id, int stateId, int userId, string eventType);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string eventType);
        Task<Dictionary<int, IEvent>> GetAllEvents();
        #endregion
    }
}
