using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;

using System.Collections.Generic;
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
            // DONE: уведомить пользователя о том, что запись не выбрана
            if (entityVM.SelectedItem is null && action != Actions.add)
            {
                entityVM.WindowParameters.StatusBarMessage = "Запись не выбрана!";
                return;
            }
            switch (action)
            {
                case Actions.add:
                    Add();
                    break;
                case Actions.update:
                    Update();
                    break;
                case Actions.remove:
                    if (Remove())
                        entityVM.WindowParameters.StatusBarMessage = "Запись успешно удалена";
                    else
                        entityVM.WindowParameters.StatusBarMessage = "Не удалось выполнить операцию";
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
        private void Add() => dialogVM.Show();

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для изменения записи в таблице
        /// </summary>
        private void Update()
        {
            dialogVM.SelectedIndex = entityVM.SelectedIndex;
            dialogVM.DialogItem = ((T)entityVM.SelectedItem).Clone();
            dialogVM.SelectedItem = (T)entityVM.SelectedItem;

            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет удаление выбранной записи в таблице
        /// </summary>
        private bool Remove()
        {
            // хранит значение индекса, чтобы заново его присвоить
            int selectedIndex = entityVM.SelectedIndex;
            T selectedEmployee = (T)entityVM.SelectedItem;

            // DONE: Проверить чтобы элемент не удалялся из таблицы в случае не успеха

            // удаление в базе данных
            if (selectedEmployee.Remove())
            {
                // удаление в коллекции
                itemsList.Remove(selectedEmployee);
            }
            else
            { 
                return false; 
            }

            // обновление представления      
            CollectionViewSource.GetDefaultView(itemsList).Refresh();

            entityVM.SelectedIndex = selectedIndex < itemsList.Count ? selectedIndex : selectedIndex - 1;

            return true;
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для копирования записи в таблицу
        /// </summary>
        private void Copy()
        {
            dialogVM.DialogItem = ((T)entityVM.SelectedItem).Clone();
            dialogVM.CopyParameters.CountVisibility = Visibility.visible;

            dialogVM.Show();
        }
    }
}
