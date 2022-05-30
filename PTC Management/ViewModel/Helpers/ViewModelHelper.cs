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
    internal class ViewModelHelper<T, T1>
        where T : Entity
    {
        // хранит записи, которые будут отображены в таблице
        private ObservableCollection<T> observableCollection;
        // для взаимодействия с базой данных
        private Repository<T> repository;

        public ViewModelHelper(Repository<T> repository)
        {
            this.repository = repository;
            observableCollection = repository.GetObservableCollection();
        }

        public ObservableCollection<T> ObservableCollection {
            get => observableCollection;
            set => observableCollection = value; 
        }

        /// <summary> Возвращает представление </summary>
        public ICollectionView GetItems() =>
            CollectionViewSource.GetDefaultView(observableCollection);

        /// <summary>
        /// Выполняет изменнение observableCollection,
        /// на основе заданного действия                     
        /// </summary>
        public void DoActionForObservableCollection(string MainWindowAction, int id, int selectedIndex, T item)
        {
            List<T> List;
            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAdded(id);
                    break;
                case Actions.update:
                    UpdateObservableCollection(selectedIndex, item);
                    return; // выход из функции
                case Actions.copy:
                    List = repository.GetFrom(id);
                    break;
                default: return;
            }

            foreach (var employee in List)
                observableCollection.Add(employee);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу
        /// </summary>        
        public List<T> GetAdded(int id) => new List<T>
        {
            repository.GetSingle(id)
        };

        /// <summary>
        /// Выполняет обновление записи в observableCollection
        /// и обновляет представление, используещее данную коллекцию
        /// </summary>
        private void UpdateObservableCollection(int selectedIndex, T item)
        {
            ObservableCollection<T> ob = observableCollection;

            ob[selectedIndex].SetFields(item);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }
    }
}
