﻿using PTC_Management.EntityFramework;
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
                logOfDAE.Date = DateTime.Now;

                logOfDAE.TimeOnDeparture = new TimeSpan(0);
                logOfDAE.TimeWhenReturning = new TimeSpan(0);
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
            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции отображающей записи в таблице
                ViewModelHelper.DoActionForList(
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (LogOfDepartureAndEntry)DialogItem);
            }
        }

        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel(destination, ViewModelHelper.IdTransport);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                LogOfDepartureAndEntry tempDialogItem = (LogOfDepartureAndEntry)DialogItem.Clone();
                switch (destination)
                {
                    case Destinations.itinerary:
                        tempDialogItem.Itinerary = (Itinerary)selectWindow.ReturnedItem;
                        tempDialogItem.IdItinerary = (int)((Itinerary)selectWindow.ReturnedItem).Id;
                        break;

                    default: break;
                }

                DialogItem = tempDialogItem;
            }

        }

        #endregion
    }
}
