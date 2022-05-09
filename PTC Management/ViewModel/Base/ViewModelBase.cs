using PTC_Management.Model.Dialog;
using PTC_Management.Windows;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class ViewModelBase : BindableBase
    {
        private string mainWindowAction;
        public string MainWindowAction
        { 
            get => mainWindowAction; 
            set => mainWindowAction=value; 
        }

        private Actions actions = new Actions();
        public Actions Actions { get => actions; }

        public ViewModelBase() {
            DialogCommand = new ParameterizedCommand<string>(OnDialog);
        }

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;  
        }

        public ICommand DialogCommand { get; set; }
        public virtual void OnDialog(string action) { }

        /// <summary>
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        private Dialog window = null;

        #region title
        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get { return (string)GetValue(title); }
            set { SetValue(title, value); }
        }

        public static readonly DependencyProperty title =
            DependencyProperty.Register("Title", typeof(string), typeof(ViewModelBase), new PropertyMetadata(""));
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
        protected void Show(ViewModelBase viewModel)
        {
            viewModel.window = new Dialog();
            viewModel.window.DataContext = viewModel;
            viewModel.window.Closed += (sender, e) => Closed();
            viewModel.window.ShowDialog();
        }
    }
}
