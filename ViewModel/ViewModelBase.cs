using PTC_Management;
using PTC_Management.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PTC_Management.ViewModel
{
    class ViewModelBase : BindableBase
    {
        /// <summary>
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        private CreateDialog _wnd = null;

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
            if (_wnd != null)
            {
                _wnd.Close();
                _wnd = null;
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
            viewModel._wnd = new CreateDialog();
            viewModel._wnd.DataContext = viewModel;
            viewModel._wnd.Closed += (sender, e) => Closed();
            viewModel._wnd.ShowDialog();
        }
    }
}
