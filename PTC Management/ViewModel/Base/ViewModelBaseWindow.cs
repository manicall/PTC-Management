using PTC_Management.Model.Dialog;
using PTC_Management.Windows;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseDialog : BindableBase
    {
        private string mainWindowAction;
        public string MainWindowAction
        {
            get => mainWindowAction;
            set => mainWindowAction = value;
        }

        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        /// <summary>
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        private Dialog window = null;

        #region Title
        /// <summary> Заголовок окна </summary>
        private string title;
        public string Title
        {
            get => title;
            set => title = value;
        }
        #endregion

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
        public void Show()
        {
            window = new Dialog();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}
