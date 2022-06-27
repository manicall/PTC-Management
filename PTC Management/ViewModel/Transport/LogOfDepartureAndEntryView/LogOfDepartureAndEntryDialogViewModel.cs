using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System;

namespace PTC_Management.ViewModel
{
    internal class LogOfDepartureAndEntryDialogViewModel : DialogViewModel
    {

        public LogOfDepartureAndEntryDialogViewModel()
        {
            DialogItem = new LogOfDepartureAndEntry();

            if (DialogItem is LogOfDepartureAndEntry logOfDAE)
            {
                logOfDAE.Itinerary = new Itinerary();
                logOfDAE.Itinerary.Date = DateTime.Now;
            }

            CurrentViewModel = this;
        }

        internal ViewModelHelper<LogOfDepartureAndEntry> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            if (DialogItem is LogOfDepartureAndEntry LogOfDAE)
                if (!LogOfDAE.GetCanExecute())
                {
                    LogOfDAE.SetCanExecute();
                    return;
                }

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
