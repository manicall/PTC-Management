﻿using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using PTC_Management.ViewModel.Helpers;

using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class TransportViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Transport> viewModelHelper;
        public ICommand TransportInfoCommand { get; set; }

        public string TansportInfoVisibility { get; set; }

        public TransportViewModel(string tansportInfoVisibility = Visibility.visible)
        {
            TransportInfoCommand = new Command<string>(OnTransportInfo);
            TansportInfoVisibility = tansportInfoVisibility;

            viewModelHelper =
                new ViewModelHelper<Transport>(Transport.repository);

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Transport current = entity as Transport;

            return true;
        }


        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;


        #region Методы
        public void OnTransportInfo(string destination)
        {
            // TODO: отправить сообщение о том, что должен быть выбран транспорт
            if ((Transport)SelectedItem is null) return;

            var transportInfo = new TransportInfoViewModel();

            switch (destination)
            {
                case Destinations.maintanceLog:
                    transportInfo.CurrentViewModel = new MaintanceLogViewModel((Transport)SelectedItem);
                    transportInfo.Title = "Журнал технического обслуживания";
                    transportInfo.Show();

                    break;

                case Destinations.logOfDepartureAndEntry:
                    transportInfo.CurrentViewModel = new LogOfDepartureAndEntryViewModel();
                    transportInfo.Title = "Журнал регистрации въезда и выезда";
                    transportInfo.Show();

                    break;
            }
        }

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<Transport>
                 (this, GetDialogViewModel(action),
                  viewModelHelper.ItemsList);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        public DialogViewModel GetDialogViewModel(string action)
        {
            return new TransportDialogViewModel()
            {
                MainWindowAction = action,
                Title = Title = ViewModels.GetDialogTitle(action, Destinations.transport),
                ViewModelHelper = viewModelHelper
            };
        }

        #endregion
    }
}
