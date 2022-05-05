using PTC_Management.EF;
using PTC_Management.SupportClass;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBase {
        #region SelectedItem
        public Entity SelectedItem
        {
            get { return (Entity)GetValue(SelectedEmployeeProperty); }
            set { SetValue(SelectedEmployeeProperty, value); }
        }

        public static readonly DependencyProperty SelectedEmployeeProperty =
            DependencyProperty.Register("SelectedItem", typeof(Entity), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        #region CloseCommandDependencyProperty
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
    
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        public DialogViewModel()
        {
            CloseCommand = new SimpleCommand(() => Close());
        }
    }
}
