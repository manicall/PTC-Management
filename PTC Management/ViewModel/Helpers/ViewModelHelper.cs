using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.DialogViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PTC_Management.ViewModel.Helpers
{
    internal class ViewModelHelper<T>
        where T : Entity
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

        public List<T> ItemsList {
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
                    itemsList.Add(repository.GetSingle(id));
                    break;
                case Actions.update:
                    itemsList[selectedIndex].SetFields(item);
                    break;
                case Actions.copy:
                    itemsList.AddRange(repository.GetFrom(id));
                    break;
                default: return;
            }
            GetItems().Refresh();
        }
    }
}
