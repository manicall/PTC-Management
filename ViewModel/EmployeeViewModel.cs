using PTC_Management.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : BindableBase
    {
        public EmployeeViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Employee.GetInfo());
            Items.Filter = FilterEmployee;
        }

        private bool FilterEmployee(object obj)
        {
            bool result = true;
            Employee current = obj as Employee;
            if (!string.IsNullOrWhiteSpace(FilterText) && current != null && !current.idEmployee.ToString().Contains(FilterText))
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
                current.Items.Filter = null;
                current.Items.Filter = current.FilterEmployee;

            }
        }

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(MaintanceLogViewModel), new PropertyMetadata("", FilterText_Changed));

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(MaintanceLogViewModel), new PropertyMetadata(null));

    }
}
