namespace MVVM.ViewModel
{
    internal class MainWindowViewModel : IViewModel
    {
        private IViewModel _currentViewModel {  get; set; }
        public MainWindowViewModel()
        {
            _currentViewModel = new StartScreenViewModel();
        }

        public new IViewModel SelectedViewModel
        {
            get => _currentViewModel; 
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
    }
}
