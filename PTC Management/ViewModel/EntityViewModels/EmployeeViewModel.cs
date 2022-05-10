using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBaseEntity
    {
        private ObservableCollection<Employee> observableCollection;
        private Repository<Employee> repository;

        public EmployeeViewModel()
        {
            repository = Employee.repositoryEmployee;
            observableCollection = GetEmployeeObservableCollection();

            Items = GetEmployeeItems();
            Items.Filter = Filter;
        }

        #region FilterText
       
        /// <summary>
        /// Проверка подходит заданный текст под фильтр
        /// </summary>
        /// <param name="entity">Объект, который
        /// будет проверяться фильтром</param>
        /// <returns>Подхоидт ли заданная запись под фильтр</returns>
        protected override bool Filter(object entity)
        {
            Employee current = entity as Employee;

            if (!string.IsNullOrWhiteSpace(FilterEmployeeText)
                 && !current.Id.ToString().Contains(FilterEmployeeText)
                 && (current.Surname == null ||
                     !current.Surname.Contains(FilterEmployeeText))
                 && (current.Name == null ||
                     !current.Name.Contains(FilterEmployeeText))
                 && (current.Patronymic == null ||
                     !current.Patronymic.Contains(FilterEmployeeText))
                 && (current.DriverLicense == null ||
                     !current.DriverLicense.Contains(FilterEmployeeText)))
            {
                return false;
            }
            return true;
        }
        #endregion 

        #region Методы
        /// <summary> Возвращает записи из таблицы Employee. </summary>
        /// <returns>записи из таблицы Employee.</returns>
        private ObservableCollection<Employee>
            GetEmployeeObservableCollection()
        {
            return repository.GetObservableCollection();
        }

        /// <summary> Возвращает представление </summary>
        /// <returns> Преставление на основе объекта 
        /// employeeObservableCollection</returns>
        private ICollectionView GetEmployeeItems()
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
                new ActionPerformer<Employee, ObservableCollection<Employee>>
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
        /// имеющее тип EmployeeDialogViewModel.</returns>
        private DialogViewModel GetDialogViewModel(string action)
        {
            return new EmployeeDialogViewModel()
            {
                MainWindowAction = action,
                ObservableCollection = observableCollection,
                Repository = repository
            };
        }
        #endregion
    }
}
