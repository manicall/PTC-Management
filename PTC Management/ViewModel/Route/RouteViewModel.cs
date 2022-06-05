using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class RouteViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Route> viewModelHelper;

        public RouteViewModel()
        {
            viewModelHelper =
                new ViewModelHelper<Route>(Route.repository);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Route current = entity as Route;

            if (string.IsNullOrWhiteSpace(FilterText)
                 // && !current.Id.ToString().Contains(FilterText)
                 || current.Number.ToString().Contains(FilterText)
                 || current.Name.Contains(FilterText)
                 || current.Distant != null &&
                     current.Distant.ToString().Contains(FilterText))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация представление-модель диалогового окна
            var dialogViewModel = GetDialogViewModel<RouteDialogViewModel>(action, Destinations.route);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            var actionPerformer =
                 new ActionPerformer<Route>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }

        #endregion
    }
}
