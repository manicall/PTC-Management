using PTC_Management.EF;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    /// <summary>
    /// ViewModel Для представления TransportDialogViewModel
    /// </summary>
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
            Title = "Окно " +
                Actions.GetGenetiveName(MainWindowAction) +
                " транспорта";

            CopyCount = 1;
            CopyCountVisibility = "Collapsed";

            DialogItem = new Transport();
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
        /// <param name="dialogAction">                                         
        /// Действие которое следует выполнить, для вызывающей кнопки.
        /// Значение определяется нажатой кнопкой на диалоговом окне, 
        /// через CommandParameter в xaml файле.
        /// </param>
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
                    List = GetCopied();
                    break;
                default: return;
            }

            foreach (var transport in List)
                observableCollection.Add(transport);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        /// <returns> 
        /// Сотрудник, ключ которого совпадает с заданным. 
        /// </returns>
        private List<Transport> GetAdded()
        {
            return new List<Transport>
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
            ObservableCollection<Transport> ob = observableCollection;

            ob[SelectedIndex].SetFields((Transport)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        /// <summary>
        /// Возвращает записи из таблицы,
        /// значение ключа которых больше заданного параметра. 
        /// </summary>
        private List<Transport> GetCopied()
        {
            return repository.GetFrom(DialogItem.Id);
        }
        #endregion
    }
}
