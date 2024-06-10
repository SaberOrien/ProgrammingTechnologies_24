using Data.ImplementedInterfaces;

namespace Data.AbstractInterfaces
{
    public interface IDataRepository
    {
        static IDataRepository CreateDatabase(IDataContext? dataContext = null)
        {
            return new DataRepository(dataContext ?? new DataContext());
        }


        Task<IUser> GetUser(int id);
        Task<Dictionary<int, IUser>> GetUsers();
        Task AddUser(int id, string name, string surname, string email, string userType);
        Task DeleteUser(int id);
        Task UpdateUser(int id, string name, string surname, string email, string userType);


        Task<IItem> GetItem(int id);
        Task<Dictionary<int, IItem>> GetItems();
        Task AddItem(int id, string title, int publicationYear, string author, string itemType);
        Task DeleteItem(int id);
        Task UpdateItem(int id, string title, int publicationYear, string author, string itemType);


        Task<IState> GetState(int id);
        Task<Dictionary<int, IState>> GetStates();
        Task AddState(int id, int itemId, int itemAmount);
        Task DeleteState(int id);
        Task UpdateState(int id, int itemId, int itemAmount);


        Task<IEvent> GetEvent(int id);
        Task<Dictionary<int, IEvent>> GetEvents();
        Task AddEvent(int id, int stateId, int userId, string eventType);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string eventType);
    }
}
