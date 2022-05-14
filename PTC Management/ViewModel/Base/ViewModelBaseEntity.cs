using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Size = PTC_Management.Model.MainWindow.Size;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseEntity : BindableBase
    {
        private Size mainWidowSize;
        public Size MainWidowSize
        {
            get => mainWidowSize;
            set => mainWidowSize = value;
        }

        private ICollectionView items;
        public ICollectionView Items
        {
            get => items;
            set => items = value;
        }

        #region SelectedIndex
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        #endregion

        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        public ICommand DialogCommand { get; set; }

        public ViewModelBaseEntity()
        {
            DialogCommand = new Command<string>(OnDialog);
        }


        private Entity selectedItem;

        public Entity SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
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

        // необходимо переопределить в дочернем классе
        protected virtual bool Filter(object entity)
        {
            throw new NotImplementedException();
        }

    }
}
