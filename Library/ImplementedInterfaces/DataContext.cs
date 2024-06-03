using Data.AbstractInterfaces;
using Data.Database;

namespace Data.ImplementedInterfaces
{
    internal class DataContext : IDataContext
    {
        private readonly string ConnectionString;

        public DataContext(string? connectionString = null)
        {
            if(connectionString is null)
            {
                string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                string _DBRelativePath = @"Library\Database\Library.mdf";
                string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
                Console.WriteLine(_DBRelativePath);
                Console.WriteLine(_DBPath);
                this.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
            }
            else
            {
                this.ConnectionString = connectionString;
            }
        }

        #region User
        public async Task<IUser?> GetUser(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.User? user = await Task.Run(() =>
                {
                    IQueryable<Database.User> query = from u in context.Users where u.Id == id select u;
                    return query.FirstOrDefault();
                });

                return user is not null ? new User(user.Id, user.Name, user.Surname, user.Email, user.UserType) : null;
            }
        }
        public async Task AddUser(IUser user)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.User entity = new Database.User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    UserType = user.UserType,
                };

                context.Users.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task DeleteUser(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.User toDelete = (from u in context.Users where u.Id == id select u).FirstOrDefault()!;
                context.Users.DeleteOnSubmit(toDelete);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task UpdateUser(IUser user)
        {
            using(LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.User toUpdate = (from u in context.Users where u.Id == user.Id select u).FirstOrDefault()!;
                toUpdate.Name = user.Name;
                toUpdate.Surname = user.Surname;
                toUpdate.Email = user.Email;
                toUpdate.UserType = user.UserType;

                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task<Dictionary<int, IUser>> GetAllUsers()
        {
            using(LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                IQueryable<IUser> usersQuery = from u in context.Users select new User(u.Id, u.Name, u.Surname, u.Email, u.UserType) as IUser;
                return await Task.Run(() => usersQuery.ToDictionary(k => k.Id));
            }
        }
        #endregion

        #region Item
        public async Task<IItem?>GetItem(int id)
        {
            using(LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Item? item = await Task.Run(() =>
                {
                    IQueryable<Database.Item> query = from i in context.Items where i.Id == id select i;
                    return query.FirstOrDefault();
                });

                return item is not null ? new Book(item.Id, item.Title, item.PublicationYear, item.Author, item.ItemType) : null;
            }
        }
        public async Task AddItem(IItem item)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Item entity = new Database.Item()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PublicationYear = item.PublicationYear,
                    Author = item.Author,
                    ItemType = item.ItemType
                };
                context.Items.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task DeleteItem(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Item toDelete = (from i in context.Items where i.Id == id select i).FirstOrDefault()!;
                context.Items.DeleteOnSubmit(toDelete);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task UpdateItem(IItem item)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Item toUpdate = (from i in context.Items where i.Id == item.Id select i).FirstOrDefault()!;
                toUpdate.Title = item.Title;
                toUpdate.Author = item.Author;
                toUpdate.ItemType = item.ItemType;
                toUpdate.PublicationYear = item.PublicationYear;
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task<Dictionary<int, IItem>> GetAllItems()
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                IQueryable<IItem> itemQuery = from i in context.Items select new Book(i.Id, i.Title, i.PublicationYear, i.Author, i.ItemType) as IItem;

                return await Task.Run(() => itemQuery.ToDictionary(k => k.Id));
            }
        }
        #endregion

        #region State
        public async Task<IState?> GetState(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.State? state = await Task.Run(() =>
                {
                    IQueryable<Database.State> query = from s in context.States where s.Id == id select s;
                    return query.FirstOrDefault();
                });

                return state is not null ? new State(state.Id, state.ItemId, state.ItemAmount) : null;
            }
        }
        public async Task AddState(IState state)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.State entity = new Database.State()
                {
                    Id = state.Id,
                    ItemId = state.ItemId,
                    ItemAmount = state.ItemAmount,
                };

                context.States.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task DeleteState(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.State toDelete = (from s in context.States where s.Id == id select s).FirstOrDefault()!;
                context.States.DeleteOnSubmit(toDelete);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task UpdateState(IState state)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.State toUpdate = (from s in context.States where s.Id == state.Id select s).FirstOrDefault()!;
                toUpdate.ItemId = state.ItemId;
                toUpdate.ItemAmount = state.ItemAmount;
                await Task.Run(()=>context.SubmitChanges());
            }
        }
        public async Task<Dictionary<int, IState>> GetAllStates()
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                IQueryable<IState> stateQuery = from s in context.States select new State(s.Id, s.ItemId, s.ItemAmount) as IState;
                return await Task.Run(() => stateQuery.ToDictionary(k => k.Id));
            }
        }
        #endregion

        #region Event
        public async Task<IEvent?> GetEvent(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Event? @event = await Task.Run(() =>
                {
                    IQueryable<Database.Event> query = from e in context.Events where e.Id == id select e;
                    return query.FirstOrDefault();
                });

                return @event is not null ? new Event(@event.Id, @event.StateId, @event.UserId, @event.DateStamp, @event.EventType) : null;
            }
        }
        public async Task AddEvent(IEvent @event)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Event entity = new Database.Event()
                {
                    Id = @event.Id,
                    StateId = @event.StateId,
                    UserId = @event.UserId,
                    DateStamp = @event.DateStamp,
                    EventType = @event.EventType
                };

                context.Events.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task DeleteEvent(int id)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Event toDelete = (from e in context.Events where e.Id == id select e).FirstOrDefault()!;
                context.Events.DeleteOnSubmit(toDelete);
                await Task.Run(() => context.SubmitChanges());
            }
        }
        public async Task UpdateEvent(IEvent @event)
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                Database.Event toUpdate = (from e in context.Events where e.Id == @event.Id select e).FirstOrDefault()!;
                toUpdate.StateId = @event.StateId;
                toUpdate.UserId = @event.UserId;
                toUpdate.DateStamp = @event.DateStamp;
                toUpdate.EventType = @event.EventType;

                await Task.Run(()=>context.SubmitChanges());
            }
        }
        public async Task<Dictionary<int, IEvent>> GetAllEvents()
        {
            using (LibraryDataContext context = new LibraryDataContext(this.ConnectionString))
            {
                IQueryable<IEvent> eventQuery = from e in context.Events select new Event(e.Id, e.StateId, e.UserId, e.DateStamp, e.EventType);
                return await Task.Run(() => eventQuery.ToDictionary(k => k.Id));
            }
        }
        #endregion

        #region Utils
        public async Task<bool> CheckIfUserExists(int id)
        {
            return (await this.GetUser(id) != null);
        }
        public async Task<bool> CheckIfItemExists(int id)
        {
            return (await this.GetItem(id) != null);
        }
        public async Task<bool> CheckIfStateExists(int id)
        {
            return (await this.GetState(id) != null);
        }
        public async Task<bool> CheckIfEventExists(int id)
        {
            return (await this.GetEvent(id) != null);
        }
        #endregion
    }

}
