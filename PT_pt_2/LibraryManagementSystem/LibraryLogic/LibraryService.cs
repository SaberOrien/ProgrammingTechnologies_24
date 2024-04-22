using System;
using System.Collections.Generic;
using LibraryData.Models;
using LibraryData.Repositories;

namespace LibraryLogic
{
    public class LibraryService : ILibraryService
    {
        private ICatalogRepository _catalogRepository;
        private IUserRepository _userRepository;
        private IEventRepository _eventRepository;
        private IProcessStateRepository _processStateRepository;

        public LibraryService(ICatalogRepository catalogRepository, IUserRepository userRepository,
                              IEventRepository eventRepository, IProcessStateRepository processStateRepository)
        {
            _catalogRepository = catalogRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _processStateRepository = processStateRepository;
        }

        public bool RegisterUser(User user)
        {
            if (_userRepository.GetUser(user.Id) == null)
            {
                _userRepository.AddUser(user);
                return true;
            }
            return false;
        }

        public bool RemoveUser(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user != null)
            {
                _userRepository.DeleteUser(user);
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUser(user.Id);
            if (existingUser != null)
            {
                _userRepository.UpdateUser(user);
                return true;
            }
            return false;
        }

        public bool CheckoutItem(int userId, int itemId)
        {
            var item = _catalogRepository.GetItem(itemId);
            if (item != null && item.IsAvailable)
            {
                _eventRepository.AddEvent(new TakenOut(itemId, DateTime.Now, userId));
                item.IsAvailable = false;
                _catalogRepository.UpdateItem(item);
                return true;
            }
            return false;
        }

        public bool ReturnItem(int userId, int itemId, string condition)
        {
            var item = _catalogRepository.GetItem(itemId);
            if (item != null && !item.IsAvailable)
            {
                _eventRepository.AddEvent(new Returned(itemId, DateTime.Now, userId, condition));
                item.IsAvailable = true;
                _catalogRepository.UpdateItem(item);
                return true;
            }
            return false;
        }

        public IEnumerable<Item> GetAllAvailableItems()
        {
            return _processStateRepository.GetAvailableItems();
        }

        public IEnumerable<Item> GetAllCheckedOutItems()
        {
            return _processStateRepository.GetCheckedOutItems();
        }

        public IEnumerable<Item> GetUserCheckedOutItems(int userId)
        {
            var events = _eventRepository.GetEventsByUser(userId);
            var checkedOutItems = events.OfType<TakenOut>().Where(e => !events.OfType<Returned>().Any(r => r.ItemId == e.ItemId && r.UserId == userId));
            return checkedOutItems.Select(e => _catalogRepository.GetItem(e.ItemId));
        }

        public IEnumerable<Item> GetUnreturnedItems(int userId)
        {
            var allTakenOutEvents = _eventRepository.GetEventsByUser(userId).OfType<TakenOut>();
            var allReturnedEvents = _eventRepository.GetEventsByUser(userId).OfType<Returned>();

            var unreturnedItems = allTakenOutEvents.Where(takenOut =>
                !allReturnedEvents.Any(returned => returned.ItemId == takenOut.ItemId));

            return unreturnedItems.Select(e => _catalogRepository.GetItem(e.ItemId));
        }
    }
}
