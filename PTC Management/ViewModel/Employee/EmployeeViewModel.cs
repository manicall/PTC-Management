using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System.Collections;
using System.Collections.Generic;

namespace PTC_Management.ViewModel
{
    public class EmployeeViewModel : ViewModelBaseEntity
    {
        readonly ViewModelHelper<Employee> viewModelHelper;
        public List<Employee> ItemsList { get; set; }
        /// <summary>
        /// Список выбранных элементов таблицы
        /// </summary>
        public IList SelectedItemsList { get; set; }

        public EmployeeViewModel()
        {
            viewModelHelper =
                new ViewModelHelper<Employee>(Employee.repository);

            ItemsList = viewModelHelper.ItemsList;
            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText
        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Employee current = entity as Employee;

            return string.IsNullOrWhiteSpace(FilterText)
                 || current.Surname.Contains(FilterText)
                 || current.Name.Contains(FilterText)
                 || current.Patronymic != null && current.Patronymic.Contains(FilterText)
                 || current.DriverLicense.Contains(FilterText);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация модели представления диалогового окна
            var dialogViewModel =
                GetDialogViewModel<EmployeeDialogViewModel>(action, Destinations.employee);

            dialogViewModel.ViewModelHelper = viewModelHelper;

            var actionPerformer = new ActionPerformer<Employee>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.DoAction(action);
        }

        #endregion
    }
}