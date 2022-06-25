﻿using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    public class RouteViewModel : ViewModelBaseEntity
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

            return string.IsNullOrWhiteSpace(FilterText)
                 // && !current.Id.ToString().Contains(FilterText)
                 || current.Number.ToString().Contains(FilterText)
                 || current.Name.Contains(FilterText)
                 || current.Distant.ToString().Contains(FilterText);
        }
        #endregion

        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация модели представления диалогового окна
            var dialogViewModel = GetDialogViewModel<RouteDialogViewModel>(action, Destinations.route);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            var actionPerformer =
                 new ActionPerformer<Route>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.DoAction(action);
        }

        #endregion
    }
}
