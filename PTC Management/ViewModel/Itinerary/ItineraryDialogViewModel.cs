using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        // для корректной валидации,
        // так как необходимо генерировать PropertyChanged
        // непосредственно у экземляров данных классов
        // используя метод SetProperty
        private Employee employee;
        private Transport transport;
        private Route route;

        public ItineraryDialogViewModel()
        {
            DialogItem = new Itinerary();
            Employee = new Employee();
            Transport = new Transport();
            Route = new Route();

            CurrentViewModel = this;
        }


        public Employee Employee
        {
            get { return employee; }
            set { SetProperty(ref employee, value); }
        }

        public Transport Transport
        {
            get { return transport; }
            set { SetProperty(ref transport, value); }
        }

        public Route Route
        {
            get { return route; }
            set { SetProperty(ref route, value); }
        }


        public ViewModelHelper<Itinerary> ViewModelHelper { get; set; }

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
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
            }
        }

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
                //Itinerary tempDialogItem = (Itinerary)DialogItem.Clone();
                switch (destination)
                {
                    case Destinations.employee:
                        ((Itinerary)DialogItem).Employee = Employee = (Employee)selectWindow.ReturnedItem.Clone();
                        ((Itinerary)DialogItem).IdEmployee  = ((Employee)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.route:
                        ((Itinerary)DialogItem).Route = Route = (Route)selectWindow.ReturnedItem.Clone();
                        ((Itinerary)DialogItem).IdRoute = ((Route)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.transport:
                        ((Itinerary)DialogItem).Transport = Transport = (Transport)selectWindow.ReturnedItem.Clone();
                        ((Itinerary)DialogItem).IdTransport = ((Transport)selectWindow.ReturnedItem).Id;

                        break;

                    default: break;
                }
            }

        }

        void ChangeTansportInfoVisibility(SelectWindowViewModel sw, string visibility)
        {
            (sw.CurrentViewModel as TransportViewModel).TansportInfoVisibility = visibility;
        }

        #endregion
    }
}
