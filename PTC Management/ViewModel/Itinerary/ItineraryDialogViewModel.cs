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


        private Itinerary displayedDialogItem;

        public Itinerary DisplayedDialogItem
        {
            get { return displayedDialogItem; }
            set { SetProperty(ref displayedDialogItem, value); }
        }


        public ItineraryDialogViewModel()
        {
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            CopyParameters = new CopyParameters();
            DialogItem = new Itinerary();
            DisplayedDialogItem = new Itinerary();
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

        public void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            selectWindow.CurrentViewModel = viewModels.GetViewModel(destination);

            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                Itinerary tempDialogItem = (Itinerary)DisplayedDialogItem.Clone();
                switch (destination)
                {
                    case Destinations.employee:
                        // 
                        tempDialogItem.Employee = (Employee)selectWindow.ReturnedItem;
                        ((Itinerary) DialogItem).IdEmployee = ((Employee)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.route:
                        tempDialogItem.Route = (Route)selectWindow.ReturnedItem;
                        ((Itinerary)DialogItem).IdRoute =((Route)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.transport:
                        tempDialogItem.Transport = (Transport)selectWindow.ReturnedItem;
                        ((Itinerary)DialogItem).IdTransport =((Transport)selectWindow.ReturnedItem).Id;

                        break;

                    default: break;
                }

                DisplayedDialogItem = tempDialogItem;
            }

        }

        #endregion
    }
}
