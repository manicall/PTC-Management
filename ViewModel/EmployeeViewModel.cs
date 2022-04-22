using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {

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
            EmployeeItems = CollectionViewSource.GetDefaultView(Employee.GetInfo());
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
                 && !current.idEmployee.ToString().Contains(FilterEmployeeText)
                 && (current.surname == null || !current.surname.Contains(FilterEmployeeText))
                 && (current.name == null || !current.name.Contains(FilterEmployeeText))
                 && (current.middleName == null || !current.middleName.Contains(FilterEmployeeText)))
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
