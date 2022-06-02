using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Helpers;
using PTC_Management.Views.Windows;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Itinerary> viewModelHelper;

        public ItineraryDialogViewModel()
        {
            DialogItem = new Itinerary();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Itinerary> ViewModelHelper
        {
            get => viewModelHelper;
            set => viewModelHelper = value;
        }

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
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
            }
        }

        // DONE: При добавлении записей, которых еще нет в таблице, поля становятся пустыми
        protected override void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            // устанавливается представление-модель,
            // по которой будет ясно какое представление отобразить
            selectWindow.CurrentViewModel = viewModels.GetViewModel(destination);

            // отключение видимости кнопок с журналом ТО и
            // журналом въезда и выезда у окна со списком транспорта
            if (destination == Destinations.Transport) 
                ChangeTansportInfoVisibility(selectWindow, Visibility.collapsed);

            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                Itinerary tempDialogItem = (Itinerary)DialogItem.Clone();
                switch (destination)
                {
                    case Destinations.employee:
                        tempDialogItem.Employee = (Employee)selectWindow.ReturnedItem.Clone();
                        tempDialogItem.IdEmployee = ((Employee)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.route:
                        tempDialogItem.Route = (Route)selectWindow.ReturnedItem.Clone();
                        tempDialogItem.IdRoute =((Route)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.transport:
                        tempDialogItem.Transport = (Transport)selectWindow.ReturnedItem.Clone();
                        tempDialogItem.IdTransport =((Transport)selectWindow.ReturnedItem).Id;

                        break;

                    default: break;
                }
                DialogItem = tempDialogItem;
            }

        }

        void ChangeTansportInfoVisibility(SelectWindowViewModel sw, string visibility) {
            (sw.CurrentViewModel as TransportViewModel).TansportInfoVisibility = visibility;
        }

        #endregion
    }
}
