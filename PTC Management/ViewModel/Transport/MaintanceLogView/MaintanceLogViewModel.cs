using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogViewModel : ViewModelBaseEntity
    {
        readonly ViewModelHelper<MaintanceLog> viewModelHelper;

        public MaintanceLogViewModel(int idTransport)
        {
            viewModelHelper =
                new ViewModelHelper<MaintanceLog>(
                    MaintanceLog.repository,
                    Destinations.maintanceLog,
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
            MaintanceLog current = entity as MaintanceLog;

            return string.IsNullOrWhiteSpace(FilterText)
                // && !current.Id.ToString().Contains(FilterText)
                || current.IdItinerary.ToString().Contains(FilterText)
                || current.Itinerary.Date.ToString().Contains(FilterText)
                || current.Itinerary.TimeOnDeparture.ToString().Contains(FilterText)
                || current.Itinerary.TimeWhenReturning.ToString().Contains(FilterText)
                || current.Itinerary.SpeedometerInfoOnDeparture.ToString().Contains(FilterText)
                || current.Itinerary.SpeedometerInfoWhenReturning.ToString().Contains(FilterText)
                || current.Itinerary.Mileage.ToString().Contains(FilterText)
                || current.MaintenanceType.Contains(FilterText);
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация модели представления диалогового окна
            var dialogViewModel = GetDialogViewModel<MaintanceLogDialogViewModel>(action, Destinations.maintanceLog);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            if (action == Actions.add) dialogViewModel.OnActionAdd();

            var actionPerformer = new ActionPerformer<MaintanceLog>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.DoAction(action);
        }


        #endregion
    }
}
