using PTC_Management.EF;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBaseEntity
    {
        private ObservableCollection<Employee> observableCollection;
        private Repository<Employee> repository;

        public EmployeeViewModel()
        {
            repository = Employee.repository;
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
            Employee current = entity as Employee;

            if (!string.IsNullOrWhiteSpace(FilterText)
                 && !current.Id.ToString().Contains(FilterText)
                 && (current.Surname == null ||
                     !current.Surname.Contains(FilterText))
                 && (current.Name == null ||
                     !current.Name.Contains(FilterText))
                 && (current.Patronymic == null ||
                     !current.Patronymic.Contains(FilterText))
                 && (current.DriverLicense == null ||
                     !current.DriverLicense.Contains(FilterText)))
            {
                return false;
            }
            return true;
        }
        #endregion 

        #region Методы
        /// <summary> Возвращает записи из таблицы. </summary>
        /// <returns>записи из таблицы. </returns>
        private ObservableCollection<Employee>
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
        /// для текущей ViewModel. </returns>
        private DialogViewModel GetDialogViewModel(string action)
        {
            return new EmployeeDialogViewModel()
            {
                MainWindowAction = action,

                Title = 
                Actions.GetGenetiveName(action) +
                " сотрудника",

            ObservableCollection = observableCollection,
                Repository = repository
            };
        }
        #endregion
    }
}
