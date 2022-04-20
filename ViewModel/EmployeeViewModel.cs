using PTC_Management.SupportClass;
using PTC_Management.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBase
    {

        public ICommand CreateDialogCommand { get; set; }

        private void CreateDialog(string title)
        {
            var child = new EmployeeCreateViewModel()
            {
                Title = title,
                Date = DateTime.Now
            };
            Show(child);
        }

        public EmployeeViewModel()
        {
            CreateDialogCommand = new ParameterizedCommand<string>(CreateDialog);
            EmployeeItems = CollectionViewSource.GetDefaultView(Employee.GetInfo());
            EmployeeItems.Filter = FilterEmployee;
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


        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
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

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterEmployeeText", typeof(string), typeof(EmployeeViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView EmployeeItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("EmployeeItems", typeof(ICollectionView), typeof(EmployeeViewModel), new PropertyMetadata(null));

    }
}
