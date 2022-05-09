using System;
using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using System.Collections.ObjectModel;

using System.Collections.Generic;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    /// <summary>
    /// </summary>
    internal class EmployeeDialogViewModel : DialogViewModel
    {
        #region employeeObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<Employee> employeeObservableCollection;

        public ObservableCollection<Employee> EmployeeObservableCollection
        {
            get => employeeObservableCollection;
            set => employeeObservableCollection = value;
        }
        #endregion

        #region repositoryEmployee
        /// <summary> Поле, обеспечивающее взаимодействие с таблицей в базе данных. </summary>            
        private Repository<Employee> repositoryEmployee;

        public Repository<Employee> RepositoryEmployee
        {
            get => repositoryEmployee;
            set => repositoryEmployee = value;
        }
        #endregion

        #region конструкторы
        public EmployeeDialogViewModel(ObservableCollection<Employee> employees, string action) => CurrentViewModel = this;

        #endregion

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне
        /// </summary>
        /// <remarks> 
        /// Примечание: Для вызова данного метода, 
        /// кнопка диалогового окна должна быть 
        /// привязанна к команде DialogActionCommand
        /// </remarks>
        /// <param name="dialogAction">                                         
        /// Действие которое следует выполнить,
        /// для вызывающей кнопки. 
        /// </param>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполнение метода базового класса
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
                FillEmployeeObservableCollection();
        }

        /// <summary>
        /// Выполняет изменнение employeeObservableCollection, на основе заданного действия                             
        /// </summary>
        private void FillEmployeeObservableCollection() {
            List<Employee> List = null;

            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAddedEmployee();
                    break;
                case Actions.update:
                    UpdateEmployeeObservableCollection();
                    return; // выход из функции
                case Actions.copy:
                    List = GetCopiedEmployees();
                    break;
                default: return;
            }

            foreach (var employee in List) EmployeeObservableCollection.Add(employee);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        /// <returns> Сотрудник, ключ которого совпадает с заданным. </returns>
        private List<Employee> GetAddedEmployee()
        {
            
            return new List<Employee> { RepositoryEmployee.GetSingle(DialogItem.Id) };
        }

        /// <summary>
        /// Выполняет обновление записи в employeeObservableCollection
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateEmployeeObservableCollection()
        {
            EmployeeObservableCollection[SelectedIndex].SetFields((Employee)DialogItem);
            CollectionViewSource.GetDefaultView(EmployeeObservableCollection).Refresh();
        }

        /// <summary>
        /// Возвращает записи из таблицы,
        /// значение ключа которых больше заданного параметра. 
        /// </summary>
        private List<Employee> GetCopiedEmployees()
        {
            return RepositoryEmployee.GetFrom(DialogItem.Id);
        }
        #endregion
    }
}
