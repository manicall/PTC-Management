using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System;
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
                            ["Mileage"] = "True",
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

            if (DialogItem is MaintanceLog maintance)
            {
                maintance.Itinerary = new Itinerary();
                maintance.Itinerary.Date = DateTime.Now;
            }

            CurrentViewModel = this;
        }

        internal ViewModelHelper<MaintanceLog> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            var maintance = DialogItem as MaintanceLog;
          
            if (!maintance.GetCanExecute())
            {
                maintance.SetCanExecute();
                return;
            }


            //if (MainWindowAction != Actions.update) SetMaintenceType();

            SetMaintenceType();

            // выполняет изменения в бд
            if (DoDialogActionCommand(dialogAction))
                if (dialogAction != Actions.close)
                {
                    if (MainWindowAction != Actions.Update)
                    {
                        // выполняет изменения в коллекции отображающей записи в таблице
                        ViewModelHelper.DoActionForList(
                            MainWindowAction, DialogItem.Id, SelectedIndex, (MaintanceLog)DialogItem);
                    }
                }

            if (MainWindowAction == Actions.add)
            {
                maintance.Itinerary = new Itinerary();
                maintance.Itinerary.Date = DateTime.Now;

                RaisePropertyChanged(nameof(DialogItem));
            }
        }

        void SetMaintenceType()
        {
            const string TO_1 = "ТО-1";
            const string TO_2 = "ТО-2";

            var itemsList = ViewModelHelper.ItemsList;
            if (MainWindowAction == Actions.add) itemsList.Add(DialogItem as MaintanceLog);
            itemsList = itemsList.OrderBy(i => i.IdItinerary).ToList();

            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < itemsList.Count; i++) {
                sum1 += (int)itemsList[i].Itinerary.Mileage;
                sum2 += (int)itemsList[i].Itinerary.Mileage;

                if (sum1 >= 2500) {
                    sum1 += (int)itemsList[i].Itinerary.Mileage;
                    itemsList[i].MaintenanceType = TO_1;
                    if (itemsList[i].Id != 0) itemsList[i].Update();
                    sum1 = 0;
                }

                if (sum2 >= 10000)
                {
                    sum2 += (int)itemsList[i].Itinerary.Mileage;
                    itemsList[i].MaintenanceType = TO_2;
                    if (itemsList[i].Id != 0) itemsList[i].Update();
                    sum2 = 0;
                }
            }

            //const string TO_1 = "ТО-1";
            //const string TO_2 = "ТО-2";

            //var itemsList = ViewModelHelper.ItemsList;
            //var currentML = DialogItem as MaintanceLog;

            //currentML.MaintenanceType = "";

            //var index = GetIndex(itemsList, TO_2);

            //// не найдено ТО-2
            //if (index == -1)
            //{
            //    if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 10000)
            //    {
            //        currentML.MaintenanceType = TO_2;
            //    }
            //    else  // ТО-2 еще не нужно проводить
            //    {
            //        index = GetIndex(itemsList, TO_1);

            //        if (index == -1) // Не найдено ТО-1
            //        {
            //            if (GetSum(itemsList) + currentML.Itinerary.Mileage >= 2500)
            //            {
            //                currentML.MaintenanceType = TO_1;
            //            }
            //        }
            //        else
            //        {
            //            if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
            //            {
            //                currentML.MaintenanceType = TO_1;
            //            }
            //        }
            //    }
            //}
            //else // ТО-2 найдено
            //{
            //    if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 10000)
            //    {
            //        currentML.MaintenanceType = TO_2;
            //    }
            //    // ТО-2 еще не нужно проводить
            //    else
            //    {
            //        var newIndex = GetIndex(itemsList, TO_1);

            //        if (newIndex < index)
            //        {
            //            if (GetRangeSum(itemsList, index) + currentML.Itinerary.Mileage >= 2500)
            //            {
            //                currentML.MaintenanceType = TO_1;
            //            }
            //        }
                    
            //        else
            //            if (GetRangeSum(itemsList, newIndex) + currentML.Itinerary.Mileage >= 2500)
            //        {
            //            currentML.MaintenanceType = TO_1;
            //        }

            //    }
            //}
        }

        int? GetRangeSum(List<MaintanceLog> maintanceLogs, int index)
        {
            Console.WriteLine("{0} | {1}", index, maintanceLogs.Count - index - 1);

            return maintanceLogs
                .GetRange(index + 1, maintanceLogs.Count - index - 1)
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
            var selectWindow = new SelectWindowViewModel(ViewModelHelper.IdTransport, ViewModelHelper.ItemsList);
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
