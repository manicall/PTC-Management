using System;
using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using System.Collections.ObjectModel;

using System.Collections.Generic;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        private ObservableCollection<Employee> employeeObservableCollection;
        public ObservableCollection<Employee> EmployeeObservableCollection
        {
            get => employeeObservableCollection;
            set => employeeObservableCollection = value;
        }

        private Repository<Employee> repositoryEmployee;
        public Repository<Employee> RepositoryEmployee
        {
            get => repositoryEmployee;
            set => repositoryEmployee = value;
        }

        public EmployeeDialogViewModel(ObservableCollection<Employee> employees, string action) 
        {
            Title = $"Окно {Actions.GetGenetiveName(action)} сотрудника";
            CurrentViewModel = this;
            DialogItem = new Employee();
            CurrentAction = action;
            CopyCountVisibility = "Collapsed";
            EmployeeObservableCollection = employees;
            CopyCount = 1;
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

        protected override void OnDialogActionCommand(string mainWindowAction)
        {
            Console.WriteLine(DialogItem.Id);
            // выполнение метода базового класса
            base.OnDialogActionCommand(mainWindowAction);
            Console.WriteLine(DialogItem.Id);
            FillEmployeeObservableCollection();

        } 

        private void FillEmployeeObservableCollection() {
            List<Employee> List = null;
            switch (CurrentAction)
            {
                case Actions._add:
                    List = GetAddedEmployee();
                    break;
                case Actions._copy:
                    List = GetCopiedEmployees();
                    break;
                default: return;
            }

            foreach (var employee in List) EmployeeObservableCollection.Add(employee);
        }

        private List<Employee> GetCopiedEmployees() { 
            return RepositoryEmployee.GetFrom(DialogItem.Id);
        }

        private List<Employee> GetAddedEmployee()
        {
            return new List<Employee> { RepositoryEmployee.GetSingle(DialogItem.Id) };
        }
    }
}
