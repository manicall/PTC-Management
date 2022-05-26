using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.Views.Windows;
using PTC_Management.Windows;

using System.Windows;

using Size = PTC_Management.Model.MainWindow.Size;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseWindow : BindableBase
    {
        protected ViewModels viewModels;

        protected Size size;
        public Size Size
        {
            get => size;
            set => SetProperty(ref size, value);
        }

        /// <summary>
        /// ViewModel содержимого диалогового окна
        /// </summary>
        protected BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

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
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        private Window window = null;

        #region Title
        /// <summary> Заголовок окна </summary>
        private string title;
        public string Title
        {
            get => title;
            set => title = value;
        }
        #endregion

        public ViewModelBaseWindow()
        {
            size = new Size();
            viewModels = new ViewModels(size);
        }

        /// <summary> Методы вызываемый окном при закрытии </summary>
        protected virtual void Closed() { }

        /// <summary>
        /// Метод вызываемый для закрытия окна связанного с ViewModel
        /// </summary>
        public bool Close()
        {
            var result = false;
            if (window != null)
            {
                window.Close();
                window = null;
                result = true;
            }
            return result;
        }

        /// <summary> Метод показа ViewModel в окне </summary>
        /// <param name="viewModel">
        /// Указывает какое представление 
        /// будет отображаться в диалоговом окне
        /// </param>
        public void ShowDialog()
        {
            window = new Dialog();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }

        public void ShowWindow()
        {
            window = new SelectWindow();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}
