using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.DialogViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {

        private ObservableCollection<Employee> employeeObservableCollection;
        private Repository<Employee> repositoryEmployee = Employee.repositoryEmployee; 

        public EmployeeViewModel()
        {
            employeeObservableCollection = repositoryEmployee.GetObservableCollection();
            EmployeeItems = CollectionViewSource.GetDefaultView(employeeObservableCollection);
            EmployeeItems.Filter = FilterEmployee;
        }

        #region Filter
        private bool FilterEmployee(object obj)
        {
            bool result = true;
            Employee current = obj as Employee;

            if (!string.IsNullOrWhiteSpace(FilterEmployeeText)
                 && !current.Id.ToString().Contains(FilterEmployeeText)
                 && (current.Surname == null || !current.Surname.Contains(FilterEmployeeText))
                 && (current.Name == null || !current.Name.Contains(FilterEmployeeText))
                 && (current.Patronymic == null || !current.Patronymic.Contains(FilterEmployeeText))
                 && (current.DriverLicense == null || !current.DriverLicense.Contains(FilterEmployeeText)))
            {
                result = false;
            }
            return result;
        }

        private static void FilterText_Changed(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var current = d as EmployeeViewModel;
            if (current != null)
            {
                current.EmployeeItems.Filter = null;
                current.EmployeeItems.Filter = current.FilterEmployee;
            }
        }

        public string FilterEmployeeText
        {
            get { return (string)GetValue(filterTextProperty); }
            set { SetValue(filterTextProperty, value); }
        }

        public static readonly DependencyProperty filterTextProperty =
            DependencyProperty.Register(MyLiterals<Employee>.FilterText, typeof(string),
                typeof(EmployeeViewModel), new PropertyMetadata("", FilterText_Changed));

        #endregion

        #region Items
        public ICollectionView EmployeeItems
        {
            get { return (ICollectionView)GetValue(_itemsProperty); }
            set { SetValue(_itemsProperty, value); }
        }

        public static readonly DependencyProperty _itemsProperty =
            DependencyProperty.Register(MyLiterals<Employee>.Items, typeof(ICollectionView),
                typeof(EmployeeViewModel), new PropertyMetadata(null));

        #endregion

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int),
                typeof(EmployeeViewModel), new PropertyMetadata(null));

        public override void OnDialog(string action)
        {
            DialogViewModel dialog = new EmployeeDialogViewModel()
            {
                Title = $"Окно {Actions.GetGenetiveName(action)} сотрудника",
                DialogItem = new Employee(),

                MainWindowAction = action,
                CopyCountVisibility = "Collapsed",
                EmployeeObservableCollection = employeeObservableCollection,
                CopyCount = 1,
                RepositoryEmployee = repositoryEmployee
            };

            switch (action)
            {
                case Actions.add: Add(dialog); break;
                case Actions.update:
                    Update(dialog);
                    break;
                case Actions.remove:
                    if (SelectedItem is null) return;
                    Remove();
                    break;
                case Actions.copy:
                    Copy(dialog);
                    break;
            }
        }

        private void Add(DialogViewModel dialog)
        {
            Show(dialog);
        }

        private void Update(DialogViewModel dialog)
        {
            if (SelectedItem is null) return;

            dialog.SelectedIndex = SelectedIndex;
            dialog.DialogItem = ((Employee)SelectedItem).Clone();

            Show(dialog);
        }

        private void Remove()
        {
            int temp = SelectedIndex;

            Employee selectedEmployee = (Employee)SelectedItem;
            employeeObservableCollection.Remove(selectedEmployee);
            selectedEmployee.Remove();

            SelectedIndex = temp;
        }


        private void Copy(DialogViewModel dialog)
        {
            if (SelectedItem is null) return;

            dialog.DialogItem = ((Employee)SelectedItem).Clone();
            dialog.CopyCountVisibility = "Visible";

            Show(dialog);
        }

    }
}
