using System;
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
        /// <summary>
        /// Устанавливает значение свойства генерируя событие PropertyChanged
        /// </summary>
        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {

            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Вызывается при генерации события PropertyChanged
        /// </summary>
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}