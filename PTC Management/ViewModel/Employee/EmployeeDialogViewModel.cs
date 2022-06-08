using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        public EmployeeDialogViewModel()
        {
            DialogItem = new Employee();
            CurrentViewModel = this;
        }

        public ViewModelHelper<Employee> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции отображающей записи в таблице
                ViewModelHelper.DoActionForList(
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (Employee)DialogItem);
            }
        }

        #endregion
    }
}
