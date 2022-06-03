using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class ItineraryViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Itinerary> viewModelHelper;

        public ItineraryViewModel()
        {
            viewModelHelper = new ViewModelHelper<Itinerary>(Itinerary.repository);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        public ItineraryViewModel(int id)
        {
            viewModelHelper =
                new ViewModelHelper<Itinerary>(
                    Itinerary.repository,
                    Destinations.itinerary,
                    id);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Itinerary current = entity as Itinerary;
            return true;
        }
        #endregion


        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            // инициализация представление-модель диалогового окна
            DialogViewModel dialogViewModel = GetDialogViewModel<ItineraryDialogViewModel>(action, Destinations.itinerary);
            (dialogViewModel as ItineraryDialogViewModel).ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<Itinerary>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }


        #endregion
    }
}
