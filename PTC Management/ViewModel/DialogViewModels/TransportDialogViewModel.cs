using PTC_Management.EF;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class TransportDialogViewModel : DialogViewModel
    {
        public TransportDialogViewModel(string action)
        {
            Title = $"Окно {Actions.GetGenetiveName(action)} транспорта";
            CurrentViewModel = this;
            DialogItem = new Employee();
            MainWindowAction = action;
        }

        public TransportDialogViewModel(Transport employee, string action) : this(action)
        {
            DialogItem = employee;
        }
    }
}
