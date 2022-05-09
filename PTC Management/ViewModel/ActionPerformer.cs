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
    class ActionPerformer<T>
    {
        private ViewModelBaseEntity entityVM;
        private DialogViewModel dialogVM;
        private string action;

        public ActionPerformer(ViewModelBaseEntity entityVM, DialogViewModel dialogVM, string action)
        {
            this.entityVM = entityVM;
            this.dialogVM = dialogVM;
            this.action = action;
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
