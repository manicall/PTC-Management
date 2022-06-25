using PTC_Management.Commands;
using PTC_Management.Model;

using System.Windows;

using Size = PTC_Management.Model.Size;

namespace PTC_Management.ViewModel
{
    public class ViewModelBaseWindow : ViewModelBase
    {

        /// <summary>
        /// Окно в котором показывается представление
        /// </summary>
        protected Window window = null;

        /// <summary> Заголовок окна </summary>
        public string Title { get; set; }

        /// <summary>
        /// Комадна вызывающая закрытие окна
        /// </summary>
        public Command CloseCommand { get; private set; }

        public ViewModelBaseWindow()
        {
            CloseCommand = new Command(() => Close());
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
