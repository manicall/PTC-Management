﻿using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.Collections.ObjectModel;

namespace PTC_Management.ViewModel
{
    internal class MaintanceLogViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<MaintanceLog, ObservableCollection<MaintanceLog>> viewModelHelper;

        public MaintanceLogViewModel()
        {
            viewModelHelper =
                new ViewModelHelper<MaintanceLog,
                    ObservableCollection<MaintanceLog>>(MaintanceLog.repository);

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
            var actionPerformer =
                 new ActionPerformer<MaintanceLog, ObservableCollection<MaintanceLog>>
                 (this, GetDialogViewModel(action),
                  viewModelHelper.ObservableCollection);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        public DialogViewModel GetDialogViewModel(string action)
        {
            return new MaintanceLogDialogViewModel()
            {
                MainWindowAction = action,
                Title = ViewModels.GetDialogTitle(action, Destinations.maintanceLog),
                ViewModelHelper = viewModelHelper
            };
        }

        #endregion
    }
}