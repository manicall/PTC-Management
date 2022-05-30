using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class MaintanceLogDialogViewModel : DialogViewModel
    {
        #region ObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<MaintanceLog> observableCollection;

        public ObservableCollection<MaintanceLog> ObservableCollection
        {
            get => observableCollection;
            set => observableCollection = value;
        }
        #endregion

        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<MaintanceLog> repository;
        public Repository<MaintanceLog> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public MaintanceLogDialogViewModel()
        {
            CopyParameters = new CopyParameters();
            DialogItem = new MaintanceLog();
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
            List<MaintanceLog> List;
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
        private List<MaintanceLog> GetAdded() => new List<MaintanceLog>
        {
            repository.GetSingle(DialogItem.Id)
        };

        /// <summary>
        /// Выполняет обновление записи в observableCollection
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateObservableCollection()
        {
            ObservableCollection<MaintanceLog> ob = observableCollection;

            ob[SelectedIndex].SetFields((MaintanceLog)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        #endregion
    }
}
