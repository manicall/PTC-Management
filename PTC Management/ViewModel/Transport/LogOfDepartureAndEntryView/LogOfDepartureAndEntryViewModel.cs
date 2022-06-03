﻿using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class LogOfDepartureAndEntryViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<LogOfDepartureAndEntry> viewModelHelper;

        public LogOfDepartureAndEntryViewModel(Transport selectedTransport)
        {
            viewModelHelper =
                new ViewModelHelper<LogOfDepartureAndEntry>(
                    LogOfDepartureAndEntry.repository,
                    Destinations.logOfDepartureAndEntry,
                    selectedTransport.Id);

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
            // инициализация представление-модель диалогового окна
            DialogViewModel dialogViewModel = GetDialogViewModel<LogOfDepartureAndEntryDialogViewModel>(action, Destinations.logOfDepartureAndEntry);
            (dialogViewModel as LogOfDepartureAndEntryDialogViewModel).ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<LogOfDepartureAndEntry>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }


        #endregion
    }
}
