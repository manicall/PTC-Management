using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.DialogViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {
        Repository<Employee> _employee = new Repository<Employee>(new PTC_ManagementContext());

        public EmployeeViewModel()
        {

            EmployeeItems = CollectionViewSource.GetDefaultView(_employee.GetAll());
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
            get { return (string)GetValue(_filterTextProperty); }
            set { SetValue(_filterTextProperty, value); }
        }

        public static readonly DependencyProperty _filterTextProperty =
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

        public override void OnDialog(string action)
        {
            DialogViewModel dialog;
            switch (action)
            {
                case Actions._add:
                    dialog = new EmployeeDialogViewModel(action);
                    Show(dialog);
                    break;

                case Actions._update:
                    if (SelectedItem is null) return;
                    dialog = new EmployeeDialogViewModel((Employee)SelectedItem, action);
                    Show(dialog);
                    break;

                case Actions._remove:
                    if (SelectedItem is null) return;
                    ((Employee)SelectedItem).Remove();
                    break;

                case Actions._copy:
                    if (SelectedItem is null) return;
                    dialog = new EmployeeDialogViewModel((Employee)SelectedItem, action, true);
                    Show(dialog);
                    break;
            }

           
        }

    }
}
