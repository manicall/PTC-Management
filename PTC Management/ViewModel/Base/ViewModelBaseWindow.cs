using PTC_Management.Commands;
using PTC_Management.Model.MainWindow;

using System.Windows;

using Size = PTC_Management.Model.MainWindow.Size;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseWindow : ViewModelBase
    {
        protected ViewModels viewModels;

        protected Size size;
        public Size Size
        {
            get => size;
            set => SetProperty(ref size, value);
        }

        /// <summary>
        /// Окно в котором показывается текущий ViewModel
        /// </summary>
        protected Window window = null;

        public Command CloseCommand { get; private set; }

        public ViewModelBaseWindow()
        {
            CloseCommand = new Command(() => Close());

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



    }
}
