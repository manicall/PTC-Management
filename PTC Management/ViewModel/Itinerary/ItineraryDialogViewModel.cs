using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Helpers;

using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Itinerary, ObservableCollection<Itinerary>> viewModelHelper;

        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;

        public Command<string> DialogSelectСommand { get; private set; }

        public ItineraryDialogViewModel()
        {
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            CopyParameters = new CopyParameters();
            DialogItem = new Itinerary();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Itinerary, ObservableCollection<Itinerary>> ViewModelHelper
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
                viewModelHelper.DoActionForObservableCollection(
                    MainWindowAction, DialogItem.Id, SelectedIndex, (Itinerary)DialogItem);
            }
        }


        /*TODO: связять с ItineraryView*/
        public void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            selectWindow.CurrentViewModel = viewModels.GetViewModel(destination);

            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                switch (destination)
                {
                    case Destinations.employee:
                        ((Itinerary)DialogItem).Employee =
                            (Employee)selectWindow.ReturnedItem;
                        break;
                    case Destinations.route:
                        ((Itinerary)DialogItem).Route =
                            (Route)selectWindow.ReturnedItem;
                        break;
                    case Destinations.transport:
                        ((Itinerary)DialogItem).Transport =
                            (Transport)selectWindow.ReturnedItem;
                        break;

                    default: break;
                }
            }

        }

        #endregion
    }
}
