using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogDialogViewModel : DialogViewModel
    {
        private IsReadOnly isReadOnly;

        public IsReadOnly IsReadOnly
        {
            get
            {
                if (isReadOnly == null)
                {
                    IsReadOnly = new IsReadOnly()
                    {
                        Field = new Dictionary<string, string>()
                        {
                            ["Itinerary.Itinerary.Mileage"] = "True",
                            ["SpeedometerInfoOnDeparture"] = "True",
                        }
                    };
                }
                return isReadOnly;
            }
            set => isReadOnly = value;
        }

        public MaintanceLogDialogViewModel()
        {
            DialogItem = new MaintanceLog();

            if (DialogItem is MaintanceLog maintanceLog)
            {
                maintanceLog.Itinerary = new Itinerary();
            }

            CurrentViewModel = this;
        }

        public void OnActionAdd()
        {
            //var ItemsList = ViewModelHelper.ItemsList;

            //if (ItemsList.Count == 0)
            //{
            //    IsReadOnly.Field["SpeedometerInfoOnDeparture"] = "False";
            //}
            //else {
            //    if (DialogItem is MaintanceLog maintanceLog) {
            //        var SpeedometerIWR = ItemsList[ItemsList.Count - 1].Itinerary.SpeedometerInfoWhenReturning;

            //        maintanceLog.Itinerary.SpeedometerInfoOnDeparture = SpeedometerIWR;
            //    }
            //}
        }

        internal ViewModelHelper<MaintanceLog> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            var itemsList = ViewModelHelper.ItemsList;
            var currentML = DialogItem as MaintanceLog;
            var index = GetIndex(itemsList, "TO-2");

            // не найдено TO-2
            if (index == -1)
            {
                if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 10000)
                {
                    currentML.MaintenanceType = "TO-2";
                }
                else  // ТО-2 еще не нужно проводить
                {
                    index = GetIndex(itemsList, "TO-1");

                    if (index == -1) // Не найдено TO-1
                    {
                        if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 2500)
                        {
                            currentML.MaintenanceType = "TO-1";
                        }
                    }
                    else
                    {
                        if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
                        {
                            currentML.MaintenanceType = "TO-1";
                        }
                    }
                }
            }
            else // TO-2 найдено
            {
                if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 10000)
                {
                    currentML.MaintenanceType = "TO-2";
                }
                // ТО-2 еще не нужно проводить
                else
                {
                    index = GetIndex(itemsList, "TO-1");

                    if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
                    {
                        currentML.MaintenanceType = "TO-1";
                    }
                }
            }

            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции отображающей записи в таблице
                ViewModelHelper.DoActionForList(
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (MaintanceLog)DialogItem);
            }
        }

        int? GetRangeSum(List<MaintanceLog> maintanceLogs, int index)
        {
            return maintanceLogs
                .GetRange(index, maintanceLogs.Count - index - 1)
                .Sum(maintanceLog => maintanceLog.Itinerary.Mileage);
        }

        int? GetSum(List<MaintanceLog> maintanceLogs)
        {
            return maintanceLogs.Sum(maintanceLog => maintanceLog.Itinerary.Mileage);
        }

        int GetIndex(List<MaintanceLog> itemsList, string maintanceType)
        {
            return itemsList.FindLastIndex(maintanceLog =>
                maintanceLog.MaintenanceType == maintanceType);
        }

        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel(destination, ViewModelHelper.IdTransport);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                MaintanceLog tempDialogItem = (MaintanceLog)DialogItem.Clone();
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
