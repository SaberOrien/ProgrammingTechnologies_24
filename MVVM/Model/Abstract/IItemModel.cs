using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model.Abstract
{
    public interface IItemModel
    {
        int Id { get; set; }
        string Title { get; set; }
        int PublicationYear { get; set; }
        string Author { get; set; }
        string ItemType { get; set; }
    }
}
