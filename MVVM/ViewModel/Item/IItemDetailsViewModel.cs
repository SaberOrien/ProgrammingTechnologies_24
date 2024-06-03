using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public interface IItemDetailsViewModel
    {
        static IItemDetailsViewModel CreateViewModel(int id, string title, int publicationYear, string author, string itemType, IItemFunctions functions)
        {
            return new ItemDetailsViewModel(id, title, publicationYear, author, itemType, functions);
        }

        ICommand UpdateItem {  get; set; }
        int Id { get; set; }
        string Title { get; set; }
        int PublicationYear { get; set; }
        public string Author { get; set; }
        public string ItemType { get; set; }
    }
}
