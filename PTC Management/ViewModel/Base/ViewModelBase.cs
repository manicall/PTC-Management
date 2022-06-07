using PTC_Management.EntityFramework;
using PTC_Management.Model;

namespace PTC_Management.ViewModel
{
    internal class ViewModelBase : BindableBase
    {
        private ViewModelBase currentViewModel;

        private Entity selectedItem;

        /// <summary>
        /// Содержит константы, определяющие какое действие следует выполнить
        /// </summary>
        public Actions Actions { get => new Actions(); }

        /// <summary>
        /// Текущая модель представления. Определяет какое представление необходимо отобразить в окне.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        /// <summary>
        /// Выбранный элемент в таблице
        /// </summary>
        public Entity SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        /// <summary> Заголовок окна </summary>
        public string Title { get; set; }

        /// <summary>
        /// Параметры окна
        /// </summary>
        public WindowParameters WindowParameters { get; set; }
    }
}
