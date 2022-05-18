using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    /// <summary>
    /// ViewModel Для представления EmployeeDialogView
    /// </summary>
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        #region ObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<Employee> observableCollection;

        public ObservableCollection<Employee> ObservableCollection
        {
            get => observableCollection;
            set => observableCollection = value;
        }
        #endregion

        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<Employee> repository;

        public Repository<Employee> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public EmployeeDialogViewModel()
        {
            

            CopyParameters = new CopyParameters();
            DialogItem = new Employee();
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
            List<Employee> List;
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

            foreach (var employee in List)
                observableCollection.Add(employee);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        /// <returns> 
        /// Сотрудник, ключ которого совпадает с заданным. 
        /// </returns>
        private List<Employee> GetAdded()
        {
            return new List<Employee>
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
            ObservableCollection<Employee> ob = observableCollection;

            ob[SelectedIndex].SetFields((Employee)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        /// <summary>
        /// Возвращает записи из таблицы,
        /// значение ключа которых больше заданного параметра. 
        /// </summary>
        private List<Employee> GetCopied()
        {
            return repository.GetFrom(DialogItem.Id);
        }
        #endregion
    }
}
