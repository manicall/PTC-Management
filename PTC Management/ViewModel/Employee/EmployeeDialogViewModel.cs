using PTC_Management.EF;
using PTC_Management.SupportClass;
using System;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class EmployeeDialogViewModel : ViewModelBase
    {
        public Employee SelectedEmployee
        {
            get { return (Employee)GetValue(SelectedEmployeeProperty); }
            set { SetValue(SelectedEmployeeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedEmployee.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEmployeeProperty =
            DependencyProperty.Register("SelectedEmployee", typeof(Employee), typeof(EmployeeDialogViewModel), new PropertyMetadata(null));


        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(EmployeeDialogViewModel), new PropertyMetadata(null));

        public EmployeeDialogViewModel()
        {
            CloseCommand = new SimpleCommand(() => Close());
        }
    }
}
