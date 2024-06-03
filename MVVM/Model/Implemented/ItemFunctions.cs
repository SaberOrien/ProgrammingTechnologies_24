using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.Services_Abstract;
using MVVM.Model.Abstract;

namespace MVVM.Model.Implemented
{
    class ItemFunctions : IItemFunctions
    {
        private IItemService _itemService;

        public ItemFunctions(IItemService itemService)
        {
            _itemService = itemService;
        }

        private IItemModel toItemModel(IItemDTO itemDTO)
        {
            return new ItemModel(itemDTO.Id, itemDTO.Title, itemDTO.PublicationYear, itemDTO.Author, itemDTO.ItemType);
        }

        public async Task<IItemModel> GetItem(int id)
        {
            return this.toItemModel(await this._itemService.GetItem(id));
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
        public async Task<Dictionary<int, IItemModel>> GetAllItems()
        {
            Dictionary<int, IItemModel> items = new Dictionary<int, IItemModel>();
            foreach (IItemDTO item in (await this._itemService.GetItems()).Values)
            {
                items.Add(item.Id, this.toItemModel(item));
            }
            return items;
        }

    }
}
