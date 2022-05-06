using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.SupportClass;
using System;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBase {

        #region DialogItem
        public Entity DialogItem
        {
            get { return (Entity)GetValue(SelectedEmployeeProperty); }
            set { SetValue(SelectedEmployeeProperty, value); }
        }


        public static readonly DependencyProperty SelectedEmployeeProperty =
            DependencyProperty.Register("DialogItem", typeof(Entity), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        #region CloseCommandDependencyProperty
        public ICommand DialogActionCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
    
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("DialogActionCommand", typeof(ICommand), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        public DialogViewModel()
        {
            DialogActionCommand = new ParameterizedCommand<string>(OnDialogActionCommand);
        }

        private void OnDialogActionCommand(string action)
        {
            switch (action) {
                case Actions._writeAndClose:
                    DoAction(CurrentAction);
                    Close();
                    break;          
                case Actions._write:
                    DoAction(CurrentAction);
                    break;
                case Actions._close:
                    Close();
                    break;
            }
        }

        private void DoAction(string action) {
            switch (action)
            {
                case Actions._add:
                    DialogItem.Add();
                    break;
                case Actions._update:
                    DialogItem.Update();
                    break;
                case Actions._copy:

                    break;
                default: 
                    Console.WriteLine("Неизвестное действие"); 
                    break;

            }
        }


    }
}
