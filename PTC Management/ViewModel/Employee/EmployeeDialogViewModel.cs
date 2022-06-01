using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Employee> viewModelHelper;

        public EmployeeDialogViewModel()
        {
            CopyParameters = new CopyParameters();
            DialogItem = new Employee();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Employee> ViewModelHelper { 
            get => viewModelHelper;
            set => viewModelHelper = value; 
        }

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
                viewModelHelper.DoActionForList(
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Employee)DialogItem);
            }
        }

        #endregion
    }
}
