using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;

namespace PTC_Management.ViewModel
{
    public class IsReadOnly
    {
        public Dictionary<string, string> Field { get; set; }

        public static string Mileage { get => "Mileage"; }
        public static string SpeedometerInfoOnDeparture { get => "SpeedometerInfoOnDeparture"; }

        public static string True { get => "True"; }

        public static string False { get => "False"; }
    }

    internal class MaintanceLogDialogViewModel : DialogViewModel
    {
        private IsReadOnly isReadOnly;

        public IsReadOnly IsReadOnly { 
            get {
                if (isReadOnly == null) {
                    IsReadOnly = new IsReadOnly()
                    {
                        Field = new Dictionary<string, string>()
                        {
                            [IsReadOnly.Mileage] = IsReadOnly.True,
                            [IsReadOnly.SpeedometerInfoOnDeparture] = IsReadOnly.True,
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
                maintanceLog.Date = DateTime.Now;

                maintanceLog.TimeOnDeparture = new TimeSpan(0);
                maintanceLog.TimeWhenReturning = new TimeSpan(0);
            }

            CurrentViewModel = this;
        }

        public void OnActionAdd() {
            var ItemsList = ViewModelHelper.ItemsList;

            if (ItemsList.Count == 0)
            {
                IsReadOnly.Field[IsReadOnly.SpeedometerInfoOnDeparture] = IsReadOnly.False;
            }
            else {
                if (DialogItem is MaintanceLog maintanceLog) {
                    var SpeedometerIWR = ItemsList[ItemsList.Count - 1].SpeedometerInfoWhenReturning;

                    maintanceLog.SpeedometerInfoOnDeparture = SpeedometerIWR;
                    maintanceLog.SpeedometerInfoWhenReturning = SpeedometerIWR;
                }
            }
            

        }

        internal ViewModelHelper<MaintanceLog> ViewModelHelper { get; set; }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            if (MainWindowAction == Actions.copy) { 
            
                // TODO: переопределить действие, так как по умолчанию не подходит
            
            }


            var itemsList = ViewModelHelper.ItemsList;

           

            var currentML = DialogItem as MaintanceLog;

            var index = itemsList.FindLastIndex(maintanceLog => !string.IsNullOrEmpty(maintanceLog.MaintenanceType));

            if (index == -1)
            {
                if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                {
                    currentML.MaintenanceType = "TO-1";
                }
            }
            else if (itemsList[index].MaintenanceType == "TO-2")
            {

                var prevIndex = itemsList.FindLastIndex(index - 1, maintanceLog => maintanceLog.MaintenanceType == "TO-2");

                if (prevIndex == -1)
                {
                    if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                    {
                        currentML.MaintenanceType = "TO-1";
                    }
                }

                int[] sum = new int[2];

                for (int i = prevIndex; i < index; i++)
                {
                    for (int j = 0; j < sum.Length; j++)
                    {
                        sum[j] += (int)itemsList[i].Mileage;
                    }

                    if (itemsList[i].MaintenanceType == "TO-1")
                    {
                        sum[0] = 0;
                    }
                    else if (sum[0] >= 2500)
                    {
                        currentML.MaintenanceType = "TO-1";
                    }
                    else if (sum[1] >= 10000)
                    {
                        currentML.MaintenanceType = "TO-2";
                        break;
                    }


                }

            }
            else
            {
                if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                {
                    currentML.MaintenanceType = "TO-1";
                }

            }

                // не найдено TO-2
                //if (index == -1)
                //{
                //    if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 10000)
                //    {
                //        currentML.MaintenanceType = "TO-2";
                //    }
                //    else  // ТО-2 еще не нужно проводить
                //    {
                //        index = GetIndex(itemsList, "TO-1");

                //        if (index == -1) // Не найдено TO-1
                //        {
                //            if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                //            {
                //                currentML.MaintenanceType = "TO-1";
                //            }
                //        }
                //        else
                //        {
                //            if (itemsList.GetRange(index, itemsList.Count - index - 1).Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                //            {
                //                currentML.MaintenanceType = "TO-1";
                //            }
                //        }

                //    }
                //}
                //else // TO-2 найдено
                //{
                //    if (itemsList.GetRange(index, itemsList.Count - index - 1).Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 10000)
                //    {
                //        currentML.MaintenanceType = "TO-2";
                //    }
                //    // ТО-2 еще не нужно проводить
                //    else
                //    {
                //        if (itemsList.Sum(maintanceLog => maintanceLog.Mileage) + currentML.Mileage >= 2500)
                //        {
                //            currentML.MaintenanceType = "TO-1";
                //        }
                //    }
                //}

                // выполняет изменения в бд
                base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции отображающей записи в таблице
                ViewModelHelper.DoActionForList(
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (MaintanceLog)DialogItem);
            }



            
        }



        int GetIndex(List<MaintanceLog> itemsList, string maintanceType)
        {
            return itemsList.FindLastIndex(maintanceLog => maintanceLog.MaintenanceType == maintanceType);
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
                        tempDialogItem.Itinerary = (Itinerary)selectWindow.ReturnedItem.Clone();
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
