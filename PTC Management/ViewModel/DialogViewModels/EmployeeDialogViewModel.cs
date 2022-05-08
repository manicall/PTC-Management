using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Threading;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => _employees = value;
        }
        Repository<Employee> _employee = new Repository<Employee>(new PTC_ManagementContext());


        public EmployeeDialogViewModel(ObservableCollection<Employee> employees, string action) 
        {
            Title = $"Окно {Actions.GetGenetiveName(action)} сотрудника";
            CurrentViewModel = this;
            DialogItem = new Employee();
            CurrentAction = action;
            CopyCountVisibility = "Collapsed";
            Employees = employees;
            CopyCount = 3;
        }        

        public EmployeeDialogViewModel(
            Employee employee, 
            ObservableCollection<Employee> employees, 
            string action, 
            bool visibleCopyCount = false) : this(employees, action) 
        {
            DialogItem = employee;

            if (visibleCopyCount)
            {
                CopyCountVisibility = "Visible";
            }
        }

        protected override void OnDialogActionCommand(string action)
        {
            base.OnDialogActionCommand(action);
            //var List = _employee.GetFrom(DialogItem.Id);
            //foreach (var employee in List)
            //    Employees.Add(employee);
        }
    }
}
