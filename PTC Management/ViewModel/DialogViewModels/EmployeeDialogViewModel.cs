using PTC_Management.EF;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        public EmployeeDialogViewModel(string action) {
            Title = $"Окно {Actions.GetGenetiveName(action)} транспорта";
            CurrentViewModel = this;
            DialogItem = new Employee();
            CurrentAction = action;
            CopyCountVisibility = "Collapsed";
        }        

        public EmployeeDialogViewModel(Employee employee, string action, bool visibleCopyCount = false) : this(action) {
            DialogItem = employee;

            if (visibleCopyCount)
            {
                CopyCount = 1;
                CopyCountVisibility = "Visible";
            }
        }

    }
}
