using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using MVVM.Model.Implemented;

namespace MVVM.Model.Abstract
{
    public interface IItemFunctions
    {
        static IItemFunctions CreateModelOperation(IItemService? itemService = null)
        {
            return new ItemFunctions(itemService ?? IItemService.CreateItemService());
        }

        Task<IItemModel> GetItem(int id);
        Task AddItem(int id, string title, int publicationYear, string author, string itemType);
        Task DeleteItem(int id);
        Task UpdateItem(int id, string title, int publicationYear, string author, string itemType);
        Task<Dictionary<int, IItemModel>> GetAllItems();
    }
}
