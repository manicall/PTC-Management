using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        ViewModelHelper<Itinerary, List<Itinerary>> viewModelHelper;
        private readonly Destinations _destinations = new Destinations();

        public Destinations Destinations => _destinations;



        public ItineraryDialogViewModel()
        {


            CopyParameters = new CopyParameters();
            DialogItem = new Itinerary();
            DisplayedDialogItem = new Itinerary();
            CurrentViewModel = this;
        }

        internal ViewModelHelper<Itinerary, List<Itinerary>> ViewModelHelper
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

        

        #endregion
    }
}
