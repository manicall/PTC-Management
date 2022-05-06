using PTC_Management.EF;
using PTC_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : DialogViewModel
    {
        Repository<Employee> employee = new Repository<Employee>(new PTC_ManagementContext());

        public EmployeeViewModel()
        {
            Title = "Окно сотрудников";
            EmployeeItems = CollectionViewSource.GetDefaultView(employee.GetAll());
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
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register(MyLiterals<Employee>.FilterText, typeof(string),
                typeof(EmployeeViewModel), new PropertyMetadata("", FilterText_Changed));

        #endregion

        #region Items
        public ICollectionView EmployeeItems
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(MyLiterals<Employee>.Items, typeof(ICollectionView),
                typeof(EmployeeViewModel), new PropertyMetadata(null));

        #endregion

        void LoadItems() {
            EmployeeItems = CollectionViewSource.GetDefaultView(employee.GetAll());
        }


        public override void Add(Entity item)
        {
            employee.Add((Employee)item);
            if (Repository<Employee>.AutoSaveChanges) LoadItems();
        }

        public override void Update() { }

        public override void Remove(int id)
        {
            employee.Remove(id);
            if (Repository<Employee>.AutoSaveChanges) LoadItems();
        }

        public override void Copy() { }
    }
}
