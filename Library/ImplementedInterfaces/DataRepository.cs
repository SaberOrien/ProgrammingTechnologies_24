using Data.AbstractInterfaces;
using System.Diagnostics;

namespace Data.ImplementedInterfaces
{
    internal class DataRepository : IDataRepository
    {
        private IDataContext _context;
        public DataRepository(IDataContext context)
        {
            this._context = context;
        }

        #region User
        public async Task<IUser> GetUser(int id)
        {
            IUser? user = await this._context.GetUser(id);
            if(user == null)
            {
                throw new Exception("User doesn't exist");
            }
            return user;
        }
        public async Task AddUser(int id, string name, string surname, string email, string userType)
        {
            IUser user = new User(id, name, surname, email, userType);
            await this._context.AddUser(user);
        }
        public async Task DeleteUser(int id)
        {
            if(!await this.CheckIfUserExists(id))
            {
                throw new Exception("User does not exist");
            }
            await this._context.DeleteUser(id);
        }
        public async Task UpdateUser(int id, string name, string surname, string email, string userType)
        {
            IUser user = new User(id, name, surname, email, userType);
            if(!await this.CheckIfUserExists(id))
            {
                throw new Exception("User doesn't exist");
            }
            await this._context.UpdateUser(user);
        }
        public async Task<Dictionary<int, IUser>> GetAllUsers()
        {
            return await this._context.GetAllUsers();
        }
        #endregion

        #region Item
        public async Task<IItem> GetItem(int id)
        {
            IItem? item = await this._context.GetItem(id);
            if(item == null)
            {
                throw new Exception("Item doesn't exist");
            }
            return item;
        }
        public async Task AddItem(int id, string title, int publicationYear, string author, string itemType)
        {
            IItem item = new Book(id, title, publicationYear, author, itemType);
            await this._context.AddItem(item);
        }
        public async Task DeleteItem(int id)
        {
            if(!await this.CheckIfItemExists(id))
            {
                throw new Exception("This item does not exist");
            }
            await this._context.DeleteItem(id);
        }
        public async Task UpdateItem(int id, string title, int publicationYear, string author, string itemType)
        {
            IItem item = new Book(id, title, publicationYear, author, itemType);
            if(!await this.CheckIfItemExists(item.Id))
            {
                throw new Exception("Item doesn't exist");
            }
            await this._context.UpdateItem(item);
        }
        public async Task<Dictionary<int, IItem>> GetAllItems()
        {
            return await this._context.GetAllItems();
        }
        #endregion

        #region State
        public async Task<IState> GetState(int id)
        {
            IState? state = await this._context.GetState(id);
            if(state == null)
            {
                throw new Exception("State doesn't exist");
            }
            return state;
        }
        public async Task AddState(int id, int itemId, int itemAmount)
        {
            if(!await this._context.CheckIfItemExists(itemId))
            {
                throw new Exception("This item doesn't exist!");
            }

            if(itemAmount < 0)
            {
                throw new Exception("Amount must be bigger than 0");
            }

            IState state = new State(id, itemId, itemAmount);
            await this._context.AddState(state);
        }
        public async Task DeleteState(int id)
        {
            if(!await this.CheckIfStateExists(id))
            {
                throw new Exception("State doesn't exist");
            }
            await this._context.DeleteState(id);
        }
        public async Task UpdateState(int id, int itemId, int itemAmount)
        {
            if(!await this._context.CheckIfItemExists(itemId))
            {
                throw new Exception("Item doesn't exist");
            }

            if(itemAmount < 0)
            {
                throw new Exception("Item amount must be bigger than 0");
            }

            IState state = new State(id, itemId, itemAmount);
            
            if(!await this.CheckIfStateExists(state.Id)){
                throw new Exception("State doesn't exist");
            }
            await this._context.UpdateState(state);
        }
        public async Task<Dictionary<int, IState>> GetAllStates()
        {
            return await this._context.GetAllStates();
        }
        #endregion

        #region Event
        public async Task<IEvent> GetEvent(int id)
        {
            IEvent? @event = await this._context.GetEvent(id);
            if(@event == null)
            {
                throw new Exception("Event doesn't exist");
            }
            return @event;
        }
        public async Task AddEvent(int id, int stateId, int userId, string eventType)
        {
            IUser user = await this.GetUser(userId);
            IState state = await this.GetState(stateId);
            IItem item = await this.GetItem(state.ItemId);
            IEvent @event = new Event(id, stateId, userId, DateTime.Now, eventType);

            switch (eventType)
            {
                case "Borrow":
                    if (state.ItemAmount > 0)
                    {
                        state.ItemAmount -= 1;
                    }
                    else
                    {
                        throw new Exception("Item is not available for borrowing.");
                    }
                    await this.UpdateState(stateId, item.Id, state.ItemAmount);
                    //await this.UpdateUser(userId, user.Name, user.Surname, user.Email, user.UserType);
                    break;
                case "Return":
                    Dictionary<int, IEvent> events = await _context.GetAllEvents();
                    Dictionary<int, IState> states = await _context.GetAllStates();

                    int borrowCount = 0;

                    foreach (IEvent even in
                        from even in events.Values
                        from stat in states.Values
                        where even.UserId == user.Id &&
                              even.StateId == stat.Id &&
                              stat.ItemId == item.Id
                        select even)
                    {
                        if (even.EventType == "Borrow")
                            borrowCount++;
                        else if (even.EventType == "Return")
                            borrowCount--;
                    }

                    if (borrowCount <= 0)
                        throw new Exception("You have not borrowed this item!");

                    state.ItemAmount += 1;
                    await this.UpdateState(stateId, item.Id, state.ItemAmount);
                    //await this.UpdateUser(userId, user.Name, user.Surname, user.Email, user.UserType);
                    break;
                default:
                    throw new ArgumentException("Invalid event type", nameof(eventType));
            }

            await this._context.AddEvent(@event);
        }
        public async Task DeleteEvent(int id)
        {
            if(!await this.CheckIfEventExists(id))
            {
                throw new Exception("Event doesn't exist");
            }
            await this._context.DeleteEvent(id);
        }
        public async Task UpdateEvent(int id, int stateId, int userId, DateTime dateStamp, string eventType)
        {
            IEvent updateEvent = new Event(id, stateId, userId, dateStamp, eventType);
            if (!await this.CheckIfEventExists(updateEvent.Id)) 
            {
                throw new Exception("Event doesn't exist");
            }

            await this._context.UpdateEvent(updateEvent);
        }
        public async Task<Dictionary<int, IEvent>> GetAllEvents()
        {
            return await this._context.GetAllEvents();
        }
        #endregion

        #region Utils
        public async Task<bool> CheckIfUserExists(int id)
        {
            return await this._context.CheckIfUserExists(id);
        }

        public async Task<bool> CheckIfItemExists(int id)
        {
            return await this._context.CheckIfItemExists(id);
        }

        public async Task<bool> CheckIfStateExists(int id)
        {
            return await this._context.CheckIfStateExists(id);
        }

        public async Task<bool> CheckIfEventExists(int id)
        {
            return await this._context.CheckIfEventExists(id);
        }
        #endregion
    }
}
