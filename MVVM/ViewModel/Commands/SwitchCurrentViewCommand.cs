using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace MVVM.ViewModel.Commands
{
    internal class SwitchCurrentViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private string _selectedViewModel;

        public SwitchCurrentViewCommand(string selectedViewModel)
        {
            _selectedViewModel = selectedViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            UserControl control = (UserControl)parameter;

            Window parentWindow = Window.GetWindow(control);
            if(parentWindow != null)
            {
                if(parentWindow.DataContext is MainWindowViewModel mainViewModel)
                {
                    switch (this._selectedViewModel)
                    {
                        case "UserView":
                            mainViewModel.SelectedViewModel = new UserViewModel();
                            break;
                        case "ItemView":
                            mainViewModel.SelectedViewModel = new ItemViewModel();
                            break;
                        case "StateView":
                            mainViewModel.SelectedViewModel = new StateViewModel();
                            break;
                        case "EventView":
                            mainViewModel.SelectedViewModel = new EventViewModel();
                            break;
                    }
                }
            }
        }
    }

}
