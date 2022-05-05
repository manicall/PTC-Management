using PTC_Management.EF;
using PTC_Management.Windows;
using System.Windows;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class ViewModelBase : BindableBase
    {
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
        private void OnDialog(string action)
        {

            var child = new DialogViewModel() {
                SelectedItem = (Entity)_selectedItem
            };

            Show(child);
        }

        /// <summary>
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        private Dialog _window = null;

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ViewModelBase), new PropertyMetadata(""));

        /// <summary>
        /// Методы вызываемый окном при закрытии
        /// </summary>
        protected virtual void Closed()
        {

        }

        /// <summary>
        /// Методы вызываемый для закрытия окна связанного с ViewModel
        /// </summary>
        public bool Close()
        {
            var result = false;
            if (_window != null)
            {
                _window.Close();
                _window = null;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Метод показа ViewModel в окне
        /// </summary>
        /// <param name="viewModel"></param>
        protected void Show(ViewModelBase viewModel)
        {
            viewModel._window = new Dialog();
            viewModel._window.DataContext = viewModel;
            viewModel._window.Closed += (sender, e) => Closed();
            viewModel._window.ShowDialog();
        }
    }
}
