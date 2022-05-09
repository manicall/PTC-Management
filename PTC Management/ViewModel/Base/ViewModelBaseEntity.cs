using PTC_Management.EF;
using PTC_Management.Model.Dialog;

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseEntity : BindableBase
    {
        private Size mainWidowSize;
        public Size MainWidowSize { 
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
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int),
                typeof(EmployeeViewModel), new PropertyMetadata(null));
        #endregion

        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        public ICommand DialogCommand { get; set; }

        public ViewModelBaseEntity()
        {
            DialogCommand = new ParameterizedCommand<string>(OnDialog);
        } 

        private Entity _selectedItem;

        public Entity SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public virtual void OnDialog(string action) { }
    }
}
