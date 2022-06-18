using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PTC_Management.ViewModel
{
    /// <summary>
    /// Реализует интерфейс INotifyPropertyChanged
    /// </summary>
    public class BindableBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Устанавливает значение свойства генерируя событие PropertyChanged
        /// </summary>
        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (!Equals(member, val))
            {
                member = val;
                RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Вызов события PropertyChanged
        /// </summary>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}