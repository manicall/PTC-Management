using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class LogOfDepartureAndEntryViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<LogOfDepartureAndEntry> viewModelHelper;

        public LogOfDepartureAndEntryViewModel(int idTransport)
        {
            viewModelHelper =
                new ViewModelHelper<LogOfDepartureAndEntry>(
                    LogOfDepartureAndEntry.repository,
                    Destinations.logOfDepartureAndEntry,
                    idTransport);

            CopyButtonVisibility = Visibility.collapsed;

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {

            LogOfDepartureAndEntry current = entity as LogOfDepartureAndEntry;

            return string.IsNullOrWhiteSpace(FilterText)
                // && !current.Id.ToString().Contains(FilterText)
                || current.IdItinerary.ToString().Contains(FilterText)
                || current.Itinerary.Date.ToString().Contains(FilterText)
                || current.Itinerary.TimeOnDeparture.ToString().Contains(FilterText)
                || current.Itinerary.TimeWhenReturning.ToString().Contains(FilterText);
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация модели представления диалогового окна
            var dialogViewModel = GetDialogViewModel<LogOfDepartureAndEntryDialogViewModel>(action, Destinations.logOfDepartureAndEntry);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<LogOfDepartureAndEntry>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.DoAction(action);
        }


        #endregion
    }
}
