using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class LogOfDepartureAndEntryDialogViewModel : DialogViewModel
    {
        #region List
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private List<LogOfDepartureAndEntry> itemsList;

        public List<LogOfDepartureAndEntry> List
        {
            get => itemsList;
            set => itemsList = value;
        }
        #endregion

        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<LogOfDepartureAndEntry> repository;
        public Repository<LogOfDepartureAndEntry> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public LogOfDepartureAndEntryDialogViewModel()
        {
            CopyParameters = new CopyParameters();
            DialogItem = new LogOfDepartureAndEntry();
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
                DoActionForList();
            }
        }

        /// <summary>
        /// Выполняет изменнение itemsList,
        /// на основе заданного действия.                     
        /// </summary>
        private void DoActionForList()
        {
            List<LogOfDepartureAndEntry> List;
            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAdded();
                    break;
                case Actions.update:
                    UpdateList();
                    return; // выход из функции
                case Actions.copy:
                    List = repository.GetFrom(DialogItem.Id);
                    break;
                default: return;
            }

            foreach (var LogOfDepartureAndEntry in List)
                itemsList.Add(LogOfDepartureAndEntry);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        private List<LogOfDepartureAndEntry> GetAdded() => new List<LogOfDepartureAndEntry>
        {
            repository.GetSingle(DialogItem.Id)
        };

        /// <summary>
        /// Выполняет обновление записи в itemsList
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateList()
        {
            List<LogOfDepartureAndEntry> ob = itemsList;

            ob[SelectedIndex].SetFields((LogOfDepartureAndEntry)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        #endregion
    }
}
