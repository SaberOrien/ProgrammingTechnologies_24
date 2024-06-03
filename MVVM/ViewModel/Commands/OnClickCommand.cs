using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel.Commands
{
    internal class OnClickCommand : ICommand
    {
        private readonly Action<object> _onClick;
        private readonly Predicate<object>? _predicate;

        public OnClickCommand(Action<object> onClick, Predicate<object>? predicate = null)
        {
            _onClick = onClick;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null || _predicate(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _onClick.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

       
    }
}
