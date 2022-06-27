using PTC_Management.EntityFramework;
using PTC_Management.Model;

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;

namespace PTC_Management.ViewModel.Helpers
{
    /// <summary>
    /// Используется для инициализации модели представления.
    /// Выполняет действия определяемые диалоговым окном.
    /// </summary>
    internal class ViewModelHelper<T>
        where T : Entity, new()
    {
        // хранит записи, которые будут отображены в таблице
        private List<T> itemsList;
        // для взаимодействия с базой данных
        private Repository<T> repository;

        public ViewModelHelper(Repository<T> repository)
        {
            this.repository = repository;
            itemsList = repository.GetList();
        }

        public int IdTransport { get; set; }

        public ViewModelHelper(Repository<T> repository, string destination, int id)
        {
            this.repository = repository;
            IdTransport = id;
            switch (destination)
            {
                case Destinations.itinerary:
                    itemsList = repository.GetItineraries(id);
                    break;
                case Destinations.maintanceLog:
                    itemsList = repository.GetMaintanceLogs(id);
                    break;
                case Destinations.logOfDepartureAndEntry:
                    itemsList = repository.GetLogOfDepartureAndEntry(id);
                    break;
                default:
                    itemsList = repository.GetList();
                    break;
            }
        }

        public List<T> ItemsList
        {
            get => itemsList;
            set => itemsList = value;
        }

        /// <summary> Возвращает представление </summary>
        public ICollectionView GetItems() =>
            CollectionViewSource.GetDefaultView(itemsList);

        /// <summary>
        /// Выполняет изменнение itemsList,
        /// на основе заданного действия                     
        /// </summary>
        public void DoActionForList(string MainWindowAction,
            int id, int selectedIndex, T item)
        {
            if (item is MaintanceLog maintance)
            {
                itemsList.Clear();
                itemsList.AddRange(repository.GetMaintanceLogs(maintance.Itinerary.Transport.Id));
            }
            else if (item is LogOfDepartureAndEntry logOfDAE)
            {
                itemsList.Clear();
                itemsList.AddRange(repository.GetLogOfDepartureAndEntry(logOfDAE.Itinerary.Transport.Id));
            }
            else
            {
                itemsList.Clear();
                itemsList.AddRange(repository.GetList());
            }

            GetItems().Refresh();
        }

    }
}
