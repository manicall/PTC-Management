using PTC_Management.Commands;
using PTC_Management.Model;

using System.Windows;

using Size = PTC_Management.Model.Size;

namespace PTC_Management.ViewModel
{
    class ViewModelBaseWindow : ViewModelBase
    {
        /// <summary>
        /// Позволяет выбрать модель представления
        /// </summary>
        protected ViewModels viewModels;

        /// <summary>
        /// Окно в котором показывается представление
        /// </summary>
        protected Window window = null;

        /// <summary>
        /// Комадна вызывающая закрытие окна
        /// </summary>
        public Command CloseCommand { get; private set; }

        public ViewModelBaseWindow()
        {
            CloseCommand = new Command(() => Close());

            WindowParameters = new WindowParameters() { StatusBarMessage = "", WindowSize = new Size() };
            viewModels = new ViewModels(WindowParameters);
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
