﻿using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<MaintanceLog> viewModelHelper;

        public MaintanceLogViewModel(Transport selectedTransport)
        {
            viewModelHelper =
                new ViewModelHelper<MaintanceLog>(
                    MaintanceLog.repository,
                    Destinations.maintanceLog,
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
            MaintanceLog current = entity as MaintanceLog;

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
            DialogViewModel dialogViewModel = GetDialogViewModel<MaintanceLogDialogViewModel>(action, Destinations.maintanceLog);
            (dialogViewModel as MaintanceLogDialogViewModel).ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<MaintanceLog>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }


        #endregion
    }
}
