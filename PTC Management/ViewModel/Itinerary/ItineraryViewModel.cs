using PTC_Management.EF;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class ItineraryViewModel : ViewModelBaseEntity
    {
        // хранит записи, которые будут отображены в таблице
        private ObservableCollection<Itinerary> observableCollection;
        // для взаимодействия с базой данных
        private Repository<Itinerary> repository;

        public ItineraryViewModel()
        {
            repository = Itinerary.repository;
            observableCollection = repository.GetObservableCollection();

            Items = GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Itinerary current = entity as Itinerary;

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
            CollectionViewSource.GetDefaultView(observableCollection);

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<Itinerary, ObservableCollection<Itinerary>>
                 (this, GetDialogViewModel(action),
                 observableCollection);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        private DialogViewModel GetDialogViewModel(string action)
        {
            return new ItineraryDialogViewModel()
            {
                MainWindowAction = action,

                Title = Actions.GetGenetiveName(action) + " путевого листа",

                ObservableCollection = observableCollection,
                Repository = repository
            };
        }
        #endregion
    }
}
