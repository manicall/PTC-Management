using PTC_Management.Model.Dialog;

using System.Windows.Input;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseEntity : BindableBase
    {
        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        public ICommand DialogCommand { get; set; }

        public ViewModelBaseEntity()
        {
            DialogCommand = new ParameterizedCommand<string>(OnDialog);
        } 

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public virtual void OnDialog(string action) { }
    }
}
