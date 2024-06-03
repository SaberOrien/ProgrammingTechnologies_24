using Data.AbstractInterfaces;
using Logic.DTOs_Abstract;
using Logic.DTOs_Implemented;
using Logic.Services_Abstract;

namespace Logic.Services_Implemented
{
    internal class ItemService : IItemService
    {
        private IDataRepository _repository;
        public ItemService(IDataRepository repository)
        {
            this._repository = repository;
        }

        private IItemDTO ToItemDTO(IItem item)
        {
            return new ItemDTO(item.Id, item.Title, item.PublicationYear, item.Author, item.ItemType);
        }
        public async Task<IItemDTO> GetItem(int id)
        {
            return this.ToItemDTO(await this._repository.GetItem(id));
        }
        public async Task AddItem(int id, string title, int publicationYear, string author, string itemType)
        {
            await this._repository.AddItem(id, title, publicationYear, author, itemType);
        }
        public async Task DeleteItem(int id)
        {
            await this._repository.DeleteItem(id);
        }
        public async Task UpdateItem(int id, string title, int publicationYear, string author, string itemType)
        {
            await this._repository.UpdateItem(id, title, publicationYear, author, itemType);
        }
        public async Task<Dictionary<int, IItemDTO>> GetItems()
        {
            Dictionary<int, IItemDTO> items = new Dictionary<int, IItemDTO> ();
            foreach(IItem item in (await this._repository.GetAllItems()).Values)
            {
                items.Add(item.Id, this.ToItemDTO(item));
            }
            return items;
        }
    }
}
