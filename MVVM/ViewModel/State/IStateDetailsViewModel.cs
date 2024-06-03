using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public interface IStateDetailsViewModel
    {
        static IStateDetailsViewModel CreateViewModel(int id, int itemId, int itemAmount, IStateFunctions functions)
        {
            return new StateDetailsViewModel(id, itemId, itemAmount, functions);
        }
        ICommand UpdateState { get; set; }
        int Id { get; set; }
        int ItemId { get; set; }
        int ItemAmount { get; set; }
    }
}
