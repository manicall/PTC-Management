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
                            ["Itinerary.Mileage"] = "True",
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

            //if (DialogItem is MaintanceLog maintanceLog)
            //{
            //    maintanceLog.Itinerary = new Itinerary();
            //}

            CurrentViewModel = this;
        }

        internal ViewModelHelper<MaintanceLog> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            SetMaintenceType();

            // выполняет изменения в бд
            if (DoDialogActionCommand(dialogAction))
                if (dialogAction != Actions.close)
                {
                    // выполняет изменения в коллекции отображающей записи в таблице
                    ViewModelHelper.DoActionForList(
                        MainWindowAction, DialogItem.Id, SelectedIndex, (MaintanceLog)DialogItem);
                }
        }


        void SetMaintenceType()
        {
            const string TO_1 = "ТО-1";
            const string TO_2 = "ТО-2";

            var itemsList = ViewModelHelper.ItemsList;
            var currentML = DialogItem as MaintanceLog;
            var index = GetIndex(itemsList, TO_2);

            // не найдено ТО-2
            if (index == -1)
            {
                if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 10000)
                {
                    currentML.MaintenanceType = TO_2;
                }
                else  // ТО-2 еще не нужно проводить
                {
                    index = GetIndex(itemsList, TO_1);

                    if (index == -1) // Не найдено ТО-1
                    {
                        if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 2500)
                        {
                            currentML.MaintenanceType = TO_1;
                        }
                    }
                    else
                    {
                        if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
                        {
                            currentML.MaintenanceType = TO_1;
                        }
                    }
                }
            }
            else // ТО-2 найдено
            {
                if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 10000)
                {
                    currentML.MaintenanceType = TO_2;
                }
                // ТО-2 еще не нужно проводить
                else
                {
                    index = GetIndex(itemsList, TO_1);

                    if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
                    {
                        currentML.MaintenanceType = TO_1;
                    }
                }
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
                if (DialogItem is MaintanceLog maintanceLog)
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
