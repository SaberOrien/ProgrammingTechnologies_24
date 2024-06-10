using MVVM.ViewModel.Commands;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    internal class StartScreenViewModel : IViewModel
    {
        public ICommand StartAppCommand { get; set; }
        public ICommand ExitAppCommand { get; set; }

        public StartScreenViewModel()
        {
            this.StartAppCommand = new SwitchCurrentViewCommand("UserView");
            this.ExitAppCommand = new CloseAppCommand();
        }
    }
}
