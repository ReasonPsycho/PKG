using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PKG
{
    public class DES_ViewModel : INotifyPropertyChanged
    {
        // Define properties and methods for your view model here

        // Example property:
        private bool _isThreeBoxesVisible;

        public bool IsThreeBoxesVisible
        {
            get => _isThreeBoxesVisible;
            set
            {
                if (_isThreeBoxesVisible != value)
                {
                    _isThreeBoxesVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}