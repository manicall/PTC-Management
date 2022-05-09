using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Metadata;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class EmployeeViewModel : ViewModelBaseEntity
    {
        private ObservableCollection<Employee> employeeObservableCollection;
        private Repository<Employee> repositoryEmployee;

        public EmployeeViewModel()
        {
            repositoryEmployee = Employee.repositoryEmployee;
            employeeObservableCollection = GetEmployeeObservableCollection();

            Items = GetEmployeeItems();
            Items.Filter = FilterEmployee;
        }


        #region FilterText
        public string FilterEmployeeText
        {
            get { return (string)GetValue(filterTextProperty); }
            set { SetValue(filterTextProperty, value); }
        }

        public static readonly DependencyProperty filterTextProperty =
            DependencyProperty.Register(
                MyLiterals<Employee>.FilterText, typeof(string),
                typeof(EmployeeViewModel),
                new PropertyMetadata("", FilterText_Changed)
                );


        /// <summary>
        /// Проверка подходит заданный текст под фильтр
        /// </summary>
        /// <param name="obj">Объект, который
        /// будет проверяться фильтром</param>
        /// <returns>Подхоидт ли заданная запись под фильтр</returns>
        private bool FilterEmployee(object obj)
        {
            Employee current = obj as Employee;

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

        private static void FilterText_Changed(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var current = d as EmployeeViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterEmployee;
            }
        }

        #endregion 

        #region Методы

        /// <summary> Возвращает записи из таблицы Employee. </summary>
        /// <returns>записи из таблицы Employee.</returns>
        private ObservableCollection<Employee> GetEmployeeObservableCollection()
        {
            return repositoryEmployee.GetObservableCollection();
        }

        /// <summary> Возвращает представление </summary>
        /// <returns> Преставление на основе объекта 
        /// employeeObservableCollection</returns>
        private ICollectionView GetEmployeeItems()
        {
            return
                CollectionViewSource
                .GetDefaultView(employeeObservableCollection);
        }

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        /// <param name="action">Действие, которое 
        /// было выбрано в главном окне.</param>
        public override void OnDialog(string action)
        {
            ActionPerformer<EmployeeDialogViewModel> actionPerformer = 
                new ActionPerformer<EmployeeDialogViewModel>(this, GetDialogViewModel(action), action);

            switch (action)
            {
                case Actions.add: 
                    actionPerformer.Add();
                    break;
                case Actions.update:
                    actionPerformer.Update();
                    break;
                case Actions.remove:
                    if (SelectedItem is null) return;
                    Remove();
                    break;
                case Actions.copy:
                    actionPerformer.Copy();
                    break;
            }
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
                Title = $"Окно {Actions.GetGenetiveName(action)} сотрудника",
                DialogItem = new Employee(),
                MainWindowAction = action,
                CopyCount = 1,
                CopyCountVisibility = "Collapsed",
                EmployeeObservableCollection = employeeObservableCollection,
                RepositoryEmployee = repositoryEmployee
            };
        }

        /// <summary>
        /// Выполняет удаление выбранной записи в таблице.
        /// </summary>
        private void Remove()
        {
            int temp = SelectedIndex;

            Employee selectedEmployee = (Employee)SelectedItem;
            employeeObservableCollection.Remove(selectedEmployee);
            selectedEmployee.Remove();

            SelectedIndex = temp;
        }

        #endregion
    }
}
