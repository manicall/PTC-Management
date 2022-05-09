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

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для добавления записи в таблицу.
        /// </summary>
        /// <param name="dialog"> Объект диалогового окна,
        /// которое должно быть запущено.</param>
        public void Add()
        {
            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет запуск диалогового окна, 
        /// для изменения записи в таблице.
        /// </summary>
        /// <param name="dialog"> Объект диалогового окна,
        /// которое должно быть запущено.</param>
        public void Update()
        {
            if (entityVM.SelectedItem is null) return;

            dialogVM.SelectedIndex = entityVM.SelectedIndex;
            dialogVM.DialogItem = ((Employee)entityVM.SelectedItem).Clone();

            dialogVM.Show();
        }

        /// <summary>
        /// Выполняет удаление выбранной записи в таблице.
        /// </summary>
        public void Remove()
        {
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
        public void Copy()
        {
            if (entityVM.SelectedItem is null) return;

            dialogVM.DialogItem = ((Employee)entityVM.SelectedItem).Clone();
            dialogVM.CopyCountVisibility = "Visible";

            dialogVM.Show();
        }
    }
}
