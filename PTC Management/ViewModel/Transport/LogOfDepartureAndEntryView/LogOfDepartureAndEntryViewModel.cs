using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class LogOfDepartureAndEntryViewModel : ViewModelBaseEntity
    {
        // хранит записи, которые будут отображены в таблице
        private List<LogOfDepartureAndEntry> itemsList;
        // для взаимодействия с базой данных
        private Repository<LogOfDepartureAndEntry> repository;

        public LogOfDepartureAndEntryViewModel()
        {
            repository = LogOfDepartureAndEntry.repository;
            itemsList = repository.GetList();

            Items = GetItems();
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
        /// <summary> Возвращает представление. </summary>
        private ICollectionView GetItems() =>
            CollectionViewSource.GetDefaultView(itemsList);

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<LogOfDepartureAndEntry>
                    (this, GetDialogViewModel(action), itemsList);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        private DialogViewModel GetDialogViewModel(string action)
        {
            return new LogOfDepartureAndEntryDialogViewModel()
            {
                MainWindowAction = action,

                Title = ViewModels.GetDialogTitle(action, Destinations.logOfDepartureAndEntry),

                List = itemsList,
                Repository = repository
            };
        }
        #endregion
    }
}
