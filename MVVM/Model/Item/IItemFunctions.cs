using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    public interface IItemFunctions
    {
        Task<ItemModel> GetItem(int id);
        Task<Dictionary<int, ItemModel>> GetItems();
        Task AddItem(int id, string title, int publicationYear, string author, string itemType);
        Task DeleteItem(int id);
        Task UpdateItem(int id, string title, int publicationYear, string author, string itemType);
    }
}
