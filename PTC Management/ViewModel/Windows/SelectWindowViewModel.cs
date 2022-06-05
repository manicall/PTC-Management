using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.ViewModel.Base;
using PTC_Management.Views.Windows;

namespace PTC_Management.ViewModel
{
    internal class SelectWindowViewModel : ViewModelBaseWindow
    {
        private Entity returnedItem;
        public Entity ReturnedItem
        {
            get => returnedItem;
            set => SetProperty(ref returnedItem, value);
        }

        public Command DialogSelectCommand { get; private set; }

        public SelectWindowViewModel()
        {
            DialogSelectCommand = new Command(OnDialogSelectCommand);
        }

        public void OnDialogSelectCommand()
        {
            returnedItem = CurrentViewModel.SelectedItem;
            Close();
        }

        public void Show()
        {
            window = new SelectWindow();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }

    }
}

