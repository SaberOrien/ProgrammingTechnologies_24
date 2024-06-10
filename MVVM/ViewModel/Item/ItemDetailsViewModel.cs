using MVVM.Model;
using MVVM.ViewModel.Commands;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class ItemDetailsViewModel : IViewModel
    {
        public ICommand UpdateItem { get; set; }

        private readonly IItemFunctions _functions;
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private int _publicationYear;
        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged(nameof(PublicationYear));
            }
        }
        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        private string _itemType;
        public string ItemType
        {
            get => _itemType;
            set
            {
                _itemType = value;
                OnPropertyChanged(nameof(ItemType));
            }
        }

        public ItemDetailsViewModel(IItemFunctions? functions = null)
        {
            this.UpdateItem = new OnClickCommand(a => this.updateItem(), c => this.canUpdateItem());
            this._functions = functions ?? new ItemFunctions(null);//ItemFunctions.CraeteItemFunctions();
        }

        public ItemDetailsViewModel(int id, string title, int publicationYear, string author, string itemType, IItemFunctions? functions = null)
        {
            this.Id = id;
            this.Title = title;
            this.PublicationYear = publicationYear;
            this.Author = author;
            this.ItemType = itemType;

            this.UpdateItem = new OnClickCommand(a => this.updateItem(), c => this.canUpdateItem());
            this._functions = functions ?? new ItemFunctions(null);//ItemFunctionsns.CraeteItemFunctions();
        }

        private void updateItem()
        {
            Task.Run(() =>
            {
                this._functions.UpdateItem(this.Id, this.Title, this.PublicationYear, this.Author, this.ItemType);
            });
        }

        private bool canUpdateItem()
        {
            return !(string.IsNullOrWhiteSpace(this.Title) || string.IsNullOrWhiteSpace(this.Author) || string.IsNullOrWhiteSpace(this.ItemType) || this.PublicationYear <= 0);
        }
    }
}
