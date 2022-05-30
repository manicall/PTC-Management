using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class TransportDialogViewModel : DialogViewModel
    {
        #region ObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<Transport> observableCollection;
        public ObservableCollection<Transport> ObservableCollection
        {
            get => observableCollection;
            set => observableCollection = value;
        }
        #endregion

        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<Transport> repository;
        public Repository<Transport> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public TransportDialogViewModel()
        {
            CopyParameters = new CopyParameters();
            DialogItem = new Transport();
            CurrentViewModel = this;
        }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции
                // отображающей записи в таблице
                DoActionForObservableCollection();
            }
        }

        /// <summary>
        /// Выполняет изменнение observableCollection,
        /// на основе заданного действия.                     
        /// </summary>
        private void DoActionForObservableCollection()
        {
            List<Transport> List;
            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAdded();
                    break;
                case Actions.update:
                    UpdateObservableCollection();
                    return; // выход из функции
                case Actions.copy:
                    List = repository.GetFrom(DialogItem.Id);
                    break;
                default: return;
            }

            foreach (var employee in List)
                observableCollection.Add(employee);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        private List<Transport> GetAdded() => new List<Transport>
        {
            repository.GetSingle(DialogItem.Id)
        };

        /// <summary>
        /// Выполняет обновление записи в observableCollection
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateObservableCollection()
        {
            ObservableCollection<Transport> ob = observableCollection;

            ob[SelectedIndex].SetFields((Transport)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        #endregion
    }
}
