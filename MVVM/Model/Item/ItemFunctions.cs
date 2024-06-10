using Logic.DTOs_Abstract;
using Logic.Services_Abstract;

namespace MVVM.Model
{
    public class ItemFunctions
    {
        private IItemService _itemService;

        public ItemFunctions(IItemService itemService)
        {
            _itemService = itemService ?? IItemService.CreateItemService(); ;
        }

        private ItemModel ToItemModel(IItemDTO itemDTO)
        {
            return new ItemModel(itemDTO.Id, itemDTO.Title, itemDTO.PublicationYear, itemDTO.Author, itemDTO.ItemType);
        }

        public async Task<ItemModel> GetItem(int id)
        {
            return this.ToItemModel(await this._itemService.GetItem(id));
        }
        public async Task<Dictionary<int, ItemModel>> GetItems()
        {
            Dictionary<int, ItemModel> items = new Dictionary<int, ItemModel>();
            foreach (IItemDTO item in (await this._itemService.GetItems()).Values)
            {
                items.Add(item.Id, this.ToItemModel(item));
            }
            return items;
        }
        public async Task AddItem(int id, string title, int publicationYear, string author, string itemType)
        {
            await this._itemService.AddItem(id, title, publicationYear, author, itemType);
        }
        public async Task DeleteItem(int id)
        {
            await this._itemService.DeleteItem(id);
        }
        public async Task UpdateItem(int id, string title, int publicationYear, string author, string itemType)
        {
            await this._itemService.UpdateItem(id, title, publicationYear, author, itemType);
        }
    }
}
