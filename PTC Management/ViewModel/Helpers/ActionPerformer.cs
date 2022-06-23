using PTC_Management.EntityFramework;
using PTC_Management.Model;

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
        private readonly ViewModelBaseEntity entityVM;
        private readonly DialogViewModel dialogVM;
        private readonly List<T> itemsList;

        public ActionPerformer(ViewModelBaseEntity entityVM,
            DialogViewModel dialogVM, List<T> itemsList)
        {
            dialogVM.SelectedItem = (T)entityVM.SelectedItem;

            this.entityVM = entityVM;
            this.dialogVM = dialogVM;
            this.itemsList = itemsList;
        }

        /// <summary>
        /// Вызывает действие, которое необходимо выполнить
        /// </summary>
        public void DoAction(string action)
        {
            if (entityVM.SelectedItem is null && action != Actions.add)
            {
                entityVM.SetStatusBarMessage("Запись не выбрана");
                return;
            }

            switch (action)
            {
                case Actions.add: Add(); break;
                case Actions.update: Update(); break;
                case Actions.remove: Remove(); break;
                case Actions.copy: Copy(); break;
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

            // удаление в базе данных
            if (selectedEmployee.Remove())
            {
                entityVM.SetStatusBarMessage("Запись успешно удалена");
                // удаление в коллекции
                itemsList.Remove(selectedEmployee);
            }
            else
            {
                entityVM.SetStatusBarMessage("Не удалось удалить запись");
                return false;
            }

            // обновление представления      
            CollectionViewSource.GetDefaultView(itemsList).Refresh();

            // изменение индекса выбранной записи
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
