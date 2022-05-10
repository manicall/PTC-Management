using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;
using PTC_Management.ViewModel.DialogViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.ViewModel
{
    class ActionPerformer<T, T1>
        where T : Entity
        where T1 : ObservableCollection<T> 
    {
        private ViewModelBaseEntity entityVM;
        private DialogViewModel dialogVM;
        private T1 observableCollection;

        public ActionPerformer(ViewModelBaseEntity entityVM, 
            DialogViewModel dialogVM, T1 observableCollection)
        {
            this.entityVM = entityVM;
            this.dialogVM = dialogVM;
            this.observableCollection = observableCollection;
        }

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
        /// для добавления записи в таблицу.
        /// </summary>
        /// <param name="dialog"> Объект диалогового окна,
        /// которое должно быть запущено.</param>
        private void Add()
        {
            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для изменения записи в таблице.
        /// </summary>
        /// <param name="dialog"> Объект диалогового окна,
        /// которое должно быть запущено.</param>
        private void Update()
        {
            if (entityVM.SelectedItem is null) return;

            dialogVM.SelectedIndex = entityVM.SelectedIndex;
            dialogVM.DialogItem = (T)entityVM.SelectedItem;

            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет удаление выбранной записи в таблице.
        /// </summary>
        private void Remove()
        {
            if (entityVM.SelectedItem is null) return;

            // хранит значение индекса, чтобы заново его присвоить, 
            // так как во время удаления записи в observableCollection
            // выбранный индекс сбрасывается
            int temp = entityVM.SelectedIndex;

            T selectedEmployee = (T)entityVM.SelectedItem;
            observableCollection.Remove(selectedEmployee);
            selectedEmployee.Remove();

            entityVM.SelectedIndex = temp;
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для копирования записи в таблицу.
        /// </summary>
        /// <param name="dialog"> Объект диалогового окна,
        /// которое должно быть запущено.</param>
        private void Copy()
        {
            if (entityVM.SelectedItem is null) return;

            dialogVM.DialogItem = ((T)entityVM.SelectedItem).Clone();
            dialogVM.CopyCountVisibility = "Visible";

            dialogVM.Show();
        }
    }
}
