﻿using System.ComponentModel;

namespace MVVM.ViewModel
{
    public class IViewModel : INotifyPropertyChanged 
    {
        public IViewModel SelectedViewModel;

        public IViewModel Parent { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
