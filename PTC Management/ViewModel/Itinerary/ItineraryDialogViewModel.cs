using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        public ItineraryDialogViewModel()
        {
            DialogItem = new Itinerary();

            (DialogItem as Itinerary).Employee = new Employee();
            (DialogItem as Itinerary).Transport = new Transport();
            (DialogItem as Itinerary).Route = new Route();

            CurrentViewModel = this;
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
                    MainWindowAction, (int)DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
            }
        }

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
