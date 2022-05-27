using PTC_Management.EF;
using PTC_Management.Model.Dialog;

namespace PTC_Management.ViewModel.Base
{
    internal class ViewModelBase : BindableBase
    {
        /// <summary>
        /// Выбранный элемент в таблице
        /// </summary>
        protected Entity selectedItem;
        public Entity SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        /// <summary>
        /// ViewModel содержимого диалогового окна
        /// </summary>
        protected ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        #region Title
        /// <summary> Заголовок окна </summary>
        private string title;
        public string Title
        {
            get => title;
            set => title = value;
        }
        #endregion
    }
}
