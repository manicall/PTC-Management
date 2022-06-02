using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class TransportDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Transport> viewModelHelper;

        public TransportDialogViewModel()
        {
            DialogItem = new Transport();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Transport> ViewModelHelper
        {
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
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Transport)DialogItem);
            }
        }


        #endregion
    }
}
