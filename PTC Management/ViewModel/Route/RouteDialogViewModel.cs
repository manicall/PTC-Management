using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class RouteDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Route, ObservableCollection<Route>> viewModelHelper;

        public RouteDialogViewModel()
        {
            CopyParameters = new CopyParameters();
            DialogItem = new Route();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Route, ObservableCollection<Route>> ViewModelHelper
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
                viewModelHelper.DoActionForObservableCollection(
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Route)DialogItem);
            }
        }

        #endregion
    }
}
