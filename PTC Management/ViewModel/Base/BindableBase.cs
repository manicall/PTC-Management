using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PTC_Management.ViewModel.Base
{
    /// <summary>
    /// реализует интерфейс INotifyPropertyChanged
    /// </summary>
    public class BindableBase : DependencyObject, INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            // 
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}