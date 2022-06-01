using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    /// <summary>
    /// Предназначен для выполнения действий, 
    /// которые соответствуют кнопкам на главном окне
    /// </summary>
    class ActionPerformer<T>
        where T : Entity
    {
        // модель представление, 
        private ViewModelBaseEntity entityVM;
        private DialogViewModel dialogVM;
        private List<T> itemsList;

        public ActionPerformer(ViewModelBaseEntity entityVM,
            DialogViewModel dialogVM, List<T> itemsList)
        {
            this.entityVM = entityVM;
            this.dialogVM = dialogVM;
            this.itemsList = itemsList;
        }

        /// <summary>
        /// Вызывает действие, которое необходимо выполнить
        /// </summary>
        public void doAction(string action)
        {
            switch (action)
            {
                case Actions.add:
                    Add();
                    break;
                case Actions.update:
                    Update();
                    break;
                case Actions.remove:
                    Remove();
                    break;
                case Actions.copy:
                    Copy();
                    break;
            }
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для добавления записи в таблицу
        /// </summary>
        private void Add()
        {
            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для изменения записи в таблице
        /// </summary>
        private void Update()
        {
            // TODO: уведомить пользователя о том, что запись не выбрана
            if (entityVM.SelectedItem is null) return;

            dialogVM.SelectedIndex = entityVM.SelectedIndex;
            dialogVM.DialogItem = ((T)entityVM.SelectedItem).Clone();
            dialogVM.SelectedItem = (T)entityVM.SelectedItem;

            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет удаление выбранной записи в таблице
        /// </summary>
        private void Remove()
        {
            if (entityVM.SelectedItem is null) return;

            // хранит значение индекса, чтобы заново его присвоить
            int selectedIndex = entityVM.SelectedIndex;
            T selectedEmployee = (T)entityVM.SelectedItem;

            // TODO: Проверить чтобы элемент не удалялся из таблицы в случае не успеха

            // удаление в коллекции
            itemsList.Remove(selectedEmployee);
            // удаление в базе данных
            selectedEmployee.Remove();
            // обновление представления      
            CollectionViewSource.GetDefaultView(itemsList).Refresh();

            entityVM.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для копирования записи в таблицу
        /// </summary>
        private void Copy()
        {
            if (entityVM.SelectedItem is null) return;

            dialogVM.DialogItem = ((T)entityVM.SelectedItem).Clone();
            dialogVM.CopyParameters.CountVisibility = Visibility.visible;

            dialogVM.Show();
        }
    }
}
