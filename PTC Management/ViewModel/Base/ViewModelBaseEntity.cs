using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model.Dialog;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Size = PTC_Management.Model.MainWindow.Size;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseEntity : ViewModelBase
    {
        private Size mainWidowSize;
        private ICollectionView items;
        private int selectedIndex;

        public ICollectionView Items
        {
            get => items;
            set => items = value;
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        public Size MainWidowSize
        {
            get => mainWidowSize;
            set => mainWidowSize = value;
        }

        public ICommand DialogCommand { get; set; }

        public ViewModelBaseEntity()
        {
            DialogCommand = new Command<string>(OnDialog);
        }

        public virtual void OnDialog(string action) { }

        public string FilterText
        {
            get { return (string)GetValue(filterTextProperty); }
            set { SetValue(filterTextProperty, value); }
        }

        public static readonly DependencyProperty filterTextProperty =
            DependencyProperty.Register(
                "FilterText", typeof(string),
                typeof(EmployeeViewModel),
                new PropertyMetadata("", FilterText_Changed)
                );

        /// <summary>
        /// Событие вызываемое при изменение текста в поле фильтра
        /// </summary>
        private static void FilterText_Changed(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            EmployeeViewModel current = d as EmployeeViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.Filter;
            }
        }

        // Необходимо переопределить в дочернем классе
        protected virtual bool Filter(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
