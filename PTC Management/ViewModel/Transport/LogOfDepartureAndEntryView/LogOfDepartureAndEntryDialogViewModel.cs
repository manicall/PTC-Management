using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace PTC_Management.ViewModel
{
    internal class LogOfDepartureAndEntryDialogViewModel : DialogViewModel
    {
      
        public LogOfDepartureAndEntryDialogViewModel()
        {
            DialogItem = new LogOfDepartureAndEntry();

            CurrentViewModel = this;
        }

        internal ViewModelHelper<LogOfDepartureAndEntry> ViewModelHelper { get; set; }

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
                        MainWindowAction, DialogItem.Id, SelectedIndex, (LogOfDepartureAndEntry)DialogItem);
                }
        }

        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel(ViewModelHelper.IdTransport, ViewModelHelper.ItemsList);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                if (DialogItem is LogOfDepartureAndEntry maintanceLog)
                {
                    switch (destination)
                    {
                        case Destinations.itinerary:
                            maintanceLog.Itinerary = (Itinerary)selectWindow.ReturnedItem;
                            maintanceLog.IdItinerary = ((Itinerary)selectWindow.ReturnedItem).Id;
                            break;

                        default:
                            break;
                    }
                }

                // уведомление о том, что свойство было изменено
                RaisePropertyChanged("DialogItem");
            }
        }

        #endregion
    }
}
