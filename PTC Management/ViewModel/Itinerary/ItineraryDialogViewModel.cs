using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System;
using System.Collections.Generic;

namespace PTC_Management.ViewModel
{
    internal class ItineraryDialogViewModel : DialogViewModel
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
                            //["SpeedometerInfoOnDeparture"] = "True",
                        }
                    };
                }
                return isReadOnly;
            }
            set => isReadOnly = value;
        }

        public ViewModelHelper<Itinerary> ViewModelHelper { get; set; }

        public ItineraryDialogViewModel()
        {
            DialogItem = new Itinerary();

            if (DialogItem is Itinerary itinerary)
            {
                itinerary.Employee = new Employee();
                itinerary.Transport = new Transport();
                itinerary.Route = new Route();

                itinerary.Date = DateTime.Now;

                itinerary.TimeOnDeparture = new TimeSpan(0);
                itinerary.TimeWhenReturning = new TimeSpan(0);
            }

            CurrentViewModel = this;
        }

        #region методы
        /// <summary>
        /// Запрещает изменять поле SpeedometerInfoOnDeparture 
        /// и устанавливает в него значение 
        /// SpeedometerInfoWhenReturning из предыдущей записи
        /// </summary>
        public void ChangeIsReadOnly()
        {
            var ItemsList = ViewModelHelper.ItemsList;

            if (ItemsList.Count == 0 || SelectedIndex == 0)
            {
                //IsReadOnly.Field["SpeedometerInfoOnDeparture"] = "False";

                if (MainWindowAction == Actions.Add && ItemsList.Count > 0)
                {
                    if (DialogItem is Itinerary itinerary)
                    {
                        var SpeedometerIWR = ItemsList[ItemsList.Count - 1].SpeedometerInfoWhenReturning;
                        itinerary.SpeedometerInfoOnDeparture = SpeedometerIWR;
                    }
                }
            }
            else
            {
                if (SelectedItem is Itinerary itinerary)
                {
                    var SpeedometerIWR = ItemsList[SelectedIndex - 1].SpeedometerInfoWhenReturning;
                    itinerary.SpeedometerInfoOnDeparture = SpeedometerIWR;
                }
            }
        }

        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            if (MainWindowAction == Actions.copy)
            {
                if (DialogItem is Itinerary itinerary)
                {
                    var ItemsList = ViewModelHelper.ItemsList;
                    var SpeedometerIWR = ItemsList[ItemsList.Count - 1].SpeedometerInfoWhenReturning;
                    itinerary.SpeedometerInfoOnDeparture = null;
                    itinerary.SpeedometerInfoWhenReturning = null;
                }
            }

            // выполняет изменения в бд
            if (DoDialogActionCommand(dialogAction))
                if (dialogAction != Actions.close)
                {
                    // выполняет изменения в коллекции отображающей записи в таблице
                    ViewModelHelper.DoActionForList(
                        MainWindowAction, DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
                }
        }

        /// <summary>
        /// возвращает результат из таблицы 
        /// </summary>
        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel(destination);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                if (DialogItem is Itinerary itinerary)
                {
                    switch (destination)
                    {
                        case Destinations.employee:
                            itinerary.Employee = (Employee)selectWindow.ReturnedItem;
                            itinerary.IdEmployee = ((Employee)selectWindow.ReturnedItem).Id;

                            break;
                        case Destinations.route:
                            itinerary.Route = (Route)selectWindow.ReturnedItem;
                            itinerary.IdRoute = ((Route)selectWindow.ReturnedItem).Id;

                            break;
                        case Destinations.transport:
                            itinerary.Transport = (Transport)selectWindow.ReturnedItem;
                            itinerary.IdTransport = ((Transport)selectWindow.ReturnedItem).Id;

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
