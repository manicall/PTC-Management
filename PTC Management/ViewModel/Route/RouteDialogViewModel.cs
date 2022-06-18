using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;

namespace PTC_Management.ViewModel
{
    internal class RouteDialogViewModel : DialogViewModel
    {


        public RouteDialogViewModel()
        {
            DialogItem = new Route();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Route> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполняет изменения в бд
            if (DoDialogActionCommand(dialogAction))
                if (dialogAction != Actions.close)
                {
                    // выполняет изменения в коллекции отображающей записи в таблице
                    ViewModelHelper.DoActionForList(
                        MainWindowAction, DialogItem.Id, SelectedIndex, (Route)DialogItem);
                }
        }

    }

    #endregion
}
