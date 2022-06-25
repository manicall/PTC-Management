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
        where T : Entity
    {
        // хранит записи, которые будут отображены в таблице
        private List<T> itemsList;
        // для взаимодействия с базой данных
        private Repository<T> repository;

        private ViewModels viewModels;

        public ViewModelHelper(Repository<T> repository, ViewModels viewModels = null)
        {
            this.repository = repository;
            this.viewModels = viewModels;
            itemsList = repository.GetList(viewModels);
        }

        public int IdTransport { get; set; }

        public ViewModelHelper(Repository<T> repository, ViewModels viewModels, string destination, int id)
        {
            this.repository = repository;
            IdTransport = id;
            switch (destination)
            {
                case Destinations.itinerary:
                    itemsList = repository.GetItineraries(id);
                    break;
                case Destinations.maintanceLog:
                    itemsList = repository.GetMaintanceLogs(id, viewModels);
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
            switch (MainWindowAction)
            {
                case Actions.add:
                    var single = repository.GetSingle(id);

                    if (typeof(T) == typeof(Itinerary))
                    {
                        var employee = viewModels.EmployeeVM.ItemsList.Find(e => e.Id == (item as Itinerary).IdEmployee);
                        (single as Itinerary).Employee = employee;
                    }

                    itemsList.Add(single);
                    break;
                case Actions.update:
                    itemsList[selectedIndex].SetFields(item);
                    break;
                case Actions.copy:
                    itemsList.Clear();
                    itemsList.AddRange(repository.GetList());
                    break;
                default: return;
            }
            GetItems().Refresh();
        }

    }
}
