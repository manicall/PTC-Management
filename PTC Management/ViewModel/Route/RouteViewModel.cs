using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel
{
    internal class RouteViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Route, List<Route>> viewModelHelper;

        public RouteViewModel()
        {
            viewModelHelper =
                new ViewModelHelper<Route,
                    List<Route>>(Route.repository);

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

            //if (!string.IsNullOrWhiteSpace(FilterText)
            //     && !current.Id.ToString().Contains(FilterText)
            //     && (current.Surname == null ||
            //         !current.Surname.Contains(FilterText))
            //     && (current.Name == null ||
            //         !current.Name.Contains(FilterText))
            //     && (current.Patronymic == null ||
            //         !current.Patronymic.Contains(FilterText))
            //     && (current.DriverLicense == null ||
            //         !current.DriverLicense.Contains(FilterText)))
            //{
            //    return false;
            //}
            return true;
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<Route, List<Route>>
                 (this, GetDialogViewModel(action),
                  viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        public DialogViewModel GetDialogViewModel(string action)
        {
            return new RouteDialogViewModel()
            {
                MainWindowAction = action,
                Title = ViewModels.GetDialogTitle(action, Destinations.route),
                ViewModelHelper = viewModelHelper
            };
        }

        #endregion
    }
}
