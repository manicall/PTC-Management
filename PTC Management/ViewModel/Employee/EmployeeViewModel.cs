using PTC_Management.EF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {
        Repository<Employee> employee = new Repository<Employee>(new PTC_ManagementContext());

        private object selectedItem;
        public object SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (value != this.selectedItem)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                }
            }
        }

        public ICommand CreateDialogCommand { get; set; }
        public ICommand ChangeDialogCommand { get; set; }

        public EmployeeViewModel()
        {
            CreateDialogCommand = new ParameterizedCommand<string>(CreateDialog);
            ChangeDialogCommand = new ParameterizedCommand<string>(ChangeDialog);
            EmployeeItems = CollectionViewSource.GetDefaultView(employee.GetAll());
            EmployeeItems.Filter = FilterEmployee;
        }

        private void CreateDialog(string title)
        {
            var child = new EmployeeDialogViewModel()
            {
                Title = title,
            };

           
            Show(child);
        }

        private void ChangeDialog(string title)
        {
            var child = new EmployeeDialogViewModel()
            {
                Title = title,
                SelectedEmployee = (Employee)selectedItem

            };

            Show(child);
        }

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
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterEmployeeText", typeof(string), 
                typeof(EmployeeViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView EmployeeItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("EmployeeItems", typeof(ICollectionView),
                typeof(EmployeeViewModel), new PropertyMetadata(null));

    }
}
