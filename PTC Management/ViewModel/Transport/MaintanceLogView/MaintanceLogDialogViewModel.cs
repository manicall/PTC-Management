using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Helpers;

using System;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class MaintanceLogDialogViewModel : DialogViewModel
    {
        ViewModelHelper<MaintanceLog> viewModelHelper;

        public MaintanceLogDialogViewModel()
        {
            DialogItem = new MaintanceLog();
            ((MaintanceLog)DialogItem).Date = DateTime.Now;
            CurrentViewModel = this;
        }

        internal ViewModelHelper<MaintanceLog> ViewModelHelper { get; set; }

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
                    MainWindowAction, DialogItem.Id, SelectedIndex, (MaintanceLog)DialogItem);
            }
        }

        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            selectWindow.CurrentViewModel = new ItineraryViewModel(viewModelHelper.IdTransport);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                MaintanceLog tempDialogItem = (MaintanceLog)DialogItem.Clone();
                switch (destination)
                {
                    case Destinations.itinerary:
                        tempDialogItem.Itinerary = (Itinerary)selectWindow.ReturnedItem.Clone();
                        tempDialogItem.IdItinerary = ((Itinerary)selectWindow.ReturnedItem).Id;
                        break;

                    default: break;
                }

                DialogItem = tempDialogItem;
            }

        }

        #endregion
    }
}
