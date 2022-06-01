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
        private List<T> itemsList;
        // для взаимодействия с базой данных
        private Repository<T> repository;

        public ViewModelHelper(Repository<T> repository)
        {
            this.repository = repository;
            itemsList = repository.GetList();
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
                    return; // выход из функции
                case Actions.copy:
                    itemsList.AddRange(repository.GetFrom(id));
                    break;
                default: return;
            }
            GetItems().Refresh();
        }
    }
}
