﻿using PTC_Management.EF;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class RouteViewModel : ViewModelBaseEntity
    {
        private ObservableCollection<Route> observableCollection;
        private Repository<Route> repository;

        public RouteViewModel()
        {
            repository = Route.repository;
            observableCollection = GetObservableCollection();

            Items = GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит заданный текст под фильтр.
        /// </summary>
        /// <param name="entity">Объект, который
        /// будет проверяться фильтром.</param>
        /// <returns>Подхоидт ли заданная запись под фильтр. </returns>
        protected override bool Filter(object entity)
        {
            Route current = entity as Route;

            return true;
        }
        #endregion 

        #region Методы
        /// <summary> Возвращает записи из таблицы. </summary>
        /// <returns>записи из таблицы. </returns>
        private ObservableCollection<Route>
            GetObservableCollection()
        {
            return repository.GetObservableCollection();
        }

        /// <summary> Возвращает представление. </summary>
        /// <returns> Преставление на основе объекта 
        /// ObservableCollection. </returns>
        private ICollectionView GetItems()
        {
            return
                CollectionViewSource
                .GetDefaultView(observableCollection);
        }

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        /// <param name="action">Действие, которое 
        /// было выбрано в главном окне.</param>
        public override void OnDialog(string action)
        {
            var actionPerformer =
                 new ActionPerformer<Route, ObservableCollection<Route>>
                 (this, GetDialogViewModel(action),
                 observableCollection);

            actionPerformer.doAction(action);
        }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр.
        /// </summary>
        /// <param name="action">Действие, которое 
        /// было выбрано в главном окне.</param>
        /// <returns>Диалоговое окно, 
        /// для текущей ViewModel. </returns>
        private DialogViewModel GetDialogViewModel(string action)
        {
            return new RouteDialogViewModel()
            {
                MainWindowAction = action,
                ObservableCollection = observableCollection,
                Repository = repository
            };
        }
        #endregion
    }
}
