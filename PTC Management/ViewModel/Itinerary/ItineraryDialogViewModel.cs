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
                            ["Itinerary.Itinerary.Mileage"] = "True",
                            ["SpeedometerInfoOnDeparture"] = "True",
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
                IsReadOnly.Field["SpeedometerInfoOnDeparture"] = "False";
            }
            else
            {
                if (DialogItem is Itinerary itinerary)
                {
                    var SpeedometerIWR = ItemsList[SelectedIndex - 1].SpeedometerInfoWhenReturning;
                    //itinerary.SpeedometerInfoOnDeparture = SpeedometerIWR;
                    (SelectedItem as Itinerary).SpeedometerInfoOnDeparture = SpeedometerIWR;
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
                    itinerary.SpeedometerInfoOnDeparture = SpeedometerIWR;
                    itinerary.SpeedometerInfoWhenReturning = null;
                }
            }

            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции отображающей записи в таблице
                ViewModelHelper.DoActionForList(
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
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
                Itinerary tempDialogItem = (Itinerary)DialogItem.Clone();
                switch (destination)
                {
                    case Destinations.employee:
                        tempDialogItem.Employee = (Employee)selectWindow.ReturnedItem;
                        tempDialogItem.IdEmployee = (int)((Employee)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.route:
                        tempDialogItem.Route = (Route)selectWindow.ReturnedItem;
                        tempDialogItem.IdRoute = (int)((Route)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.transport:
                        tempDialogItem.Transport = (Transport)selectWindow.ReturnedItem;
                        tempDialogItem.IdTransport = (int)((Transport)selectWindow.ReturnedItem).Id;

                        break;

                    default: break;
                }
                DialogItem = tempDialogItem;
            }

        }


        #endregion
    }
}
