using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    public class WindowParameters : BindableBase
    {
        private Size windowSize;
        private string sbMessage;

        /// <summary>
        /// размеры окна под которые подстраивается таблица в окне
        /// </summary>
        public Size WindowSize { get => windowSize; set => SetProperty(ref windowSize, value); }

        /// <summary>
        /// сообщение в статус баре
        /// </summary>
        public string StatusBarMessage { get => sbMessage; set => SetProperty(ref sbMessage, value); }
    }
}
