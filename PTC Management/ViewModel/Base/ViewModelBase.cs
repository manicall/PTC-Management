using PTC_Management.EF;
using PTC_Management.Model.Dialog;

namespace PTC_Management.ViewModel.Base
{
    internal class ViewModelBase : BindableBase
    {
        private Actions actions = new Actions();
        /// <summary>
        /// ViewModel содержимого диалогового окна
        /// </summary>
        protected ViewModelBase currentViewModel;
        /// <summary>
        /// Выбранный элемент в таблице
        /// </summary>
        protected Entity selectedItem;
        /// <summary> Заголовок окна </summary>
        private string title;

        public Actions Actions { get => actions; }

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }
        public Entity SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

    }
}
