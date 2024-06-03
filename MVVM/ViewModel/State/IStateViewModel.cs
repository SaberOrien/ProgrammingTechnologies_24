using MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public interface IStateViewModel
    {
        static IStateViewModel CreateViewModel(IStateFunctions function)
        {
            return new StateViewModel(function);
        }
        ICommand CreateState { get; set; }
        ICommand RemoveState { get; set; }

        public ObservableCollection<IStateDetailsViewModel> StateDetails { get; set; }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }
        public bool StateSelected { get; set; }
        public Visibility StateDetailsVisible { get; set; }
        public IStateDetailsViewModel StateDetailsViewModel { get; set; }
    }
}
