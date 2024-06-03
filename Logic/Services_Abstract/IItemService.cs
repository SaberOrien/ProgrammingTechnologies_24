using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.Services_Implemented;


namespace Logic.Services_Abstract
{
    public interface IItemService
    {
        static IItemService CreateItemService(IDataRepository? repository = null)
        {
            return new ItemService(repository ?? IDataRepository.CreateDatabase());
        }

        Task<IItemDTO> GetItem(int id);
        Task AddItem(int id, string title, int publicationYear, string author, string itemType);
        Task DeleteItem(int id);
        Task UpdateItem(int id, string title, int publicationYear, string author, string itemType);
        Task<Dictionary<int, IItemDTO>> GetItems();

    }
}
