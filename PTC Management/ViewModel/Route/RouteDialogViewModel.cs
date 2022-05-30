using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    /// <summary>
    /// ViewModel Для представления RouteDialogView
    /// </summary>
    internal class RouteDialogViewModel 
    {
        #region ObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<Route> observableCollection;

        public ObservableCollection<Route> ObservableCollection
        {
            get => observableCollection;
            set => observableCollection = value;
        }
        #endregion

        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<Route> repository;

        public Repository<Route> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public RouteDialogViewModel()
        {
            Title = "Окно " +
                Actions.GetGenetiveName(MainWindowAction) +
                " маршрута";

            CopyParameters = new CopyParameters();
            DialogItem = new Route();
            CurrentViewModel = this;
        }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        /// <remarks> 
        /// Примечание: Для вызова данного метода, кнопка диалогового окна 
        /// должна быть привязанна к команде DialogActionCommand.
        /// </remarks>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполнение метода базового класса
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                DoActionForObservableCollection();
            }
        }

        /// <summary>
        /// Выполняет изменнение observableCollection,
        /// на основе заданного действия.                     
        /// </summary>
        private void DoActionForObservableCollection()
        {
            List<Route> List;
            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAdded();
                    break;
                case Actions.update:
                    UpdateObservableCollection();
                    return; // выход из функции
                case Actions.copy:
                    List = GetCopied();
                    break;
                default: return;
            }

            foreach (var route in List)
                observableCollection.Add(route);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        private List<Route> GetAdded()
        {
            return new List<Route>
            {
                repository.GetSingle(DialogItem.Id)
            };
        }

        /// <summary>
        /// Выполняет обновление записи в observableCollection
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateObservableCollection()
        {
            ObservableCollection<Route> ob = observableCollection;

            ob[SelectedIndex].SetFields((Route)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        /// <summary>
        /// Возвращает записи из таблицы,
        /// значение ключа которых больше заданного параметра. 
        /// </summary>
        private List<Route> GetCopied()
        {
            return repository.GetFrom(DialogItem.Id);
        }
        #endregion
    }
}
