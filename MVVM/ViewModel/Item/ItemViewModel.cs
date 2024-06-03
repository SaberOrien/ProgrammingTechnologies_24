﻿using MVVM.Model.Abstract;
using MVVM.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    class ItemViewModel : IViewModel, IItemViewModel
    {
        public ICommand CreateItem {  get; set; }
        public ICommand RemoveItem { get; set;}
        public ICommand SwitchToUser { get; set; }
        public ICommand SwitchToEvent { get; set; }
        public ICommand SwitchToState { get; set; }

        private readonly IItemFunctions _itemFunctions;
        private ObservableCollection<IItemDetailsViewModel> _items;
        public ObservableCollection<IItemDetailsViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
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

        private object _itemType;
        public object ItemType
        {
            get => _itemType;
            set
            {
                _itemType = value is ComboBoxItem item ? item.Content.ToString() : value;
                OnPropertyChanged(nameof(ItemType));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                this.ShowDetails = value ? Visibility.Visible : Visibility.Hidden;
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private Visibility _showDetails;
        public Visibility ShowDetails
        {
            get => _showDetails;
            set
            {
                _showDetails = value;
                OnPropertyChanged(nameof(ShowDetails));
            }
        }

        private IItemDetailsViewModel _details;
        public IItemDetailsViewModel Details
        {
            get => _details;
            set
            {
                _details = value;
                this.IsSelected = true;
                OnPropertyChanged(nameof(Details));
            }
        }

        public ItemViewModel(IItemFunctions? functions = null)
        {
            this.SwitchToUser = new SwitchCurrentViewCommand("UserView");
            this.SwitchToState = new SwitchCurrentViewCommand("StateView");
            this.CreateItem = new OnClickCommand(a => this.GetItem(), c => this.CanGetItem());
            this.RemoveItem = new OnClickCommand(a => this.DeleteItem());

            this.Items = new ObservableCollection<IItemDetailsViewModel>();
            this._itemFunctions = functions ?? IItemFunctions.CreateModelOperation();

            this.IsSelected = false;

            Task.Run(this.LoadItems);
        }

        private async void LoadItems()
        {
            Dictionary<int, IItemModel> Items = await this._itemFunctions.GetAllItems();
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._items.Clear();
                foreach(IItemModel item in Items.Values)
                {
                    this._items.Add(new ItemDetailsViewModel(item.Id, item.Title, item.PublicationYear, item.Author, item.ItemType));
                }
            });
            OnPropertyChanged(nameof(Items));
        }

        private void GetItem()
        {
            Task.Run(async () =>
            {
                var items = await this._itemFunctions.GetAllItems();
                int currentId = items.Count + 1;
                await this._itemFunctions.AddItem(currentId, this.Title, this.PublicationYear, this.Author, this.ItemType.ToString());
                this.LoadItems();
            });
        }

        private bool CanGetItem()
        {
            return !(string.IsNullOrWhiteSpace(this.Title) || this.PublicationYear <= 0 || string.IsNullOrWhiteSpace(this.Author) || this.ItemType == null);
        }

        private void DeleteItem()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._itemFunctions.DeleteItem(this.Details.Id);
                    this.LoadItems();
                } catch (Exception e){ }
            });
        }
    }
}