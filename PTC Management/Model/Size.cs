using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    /// <summary>
    /// Параметры определяющие размеры элемента управления или окна
    /// </summary>
    internal class Size : BindableBase
    {
        public const int defaultHeightDiff = 170;
        public const int transportInfoHeightDiff = 100;
        public const int selectHeightDiff = 70;

        public const int defaultWidth = 1000;
        public const int defaultHeight = 500;

        private int width;
        private int height;

        public Size(
            int width = defaultWidth,
            int height = defaultHeight)
        {
            this.width = width;
            this.height = height;
            HeightDiff = defaultHeightDiff;
        }

        /// <summary>
        /// Определяет разницу с которой необходимо вернуть значение высоты.
        /// Данное свойство используется для того, 
        /// чтобы подстраивать максимальную высоту таблицы под высоту окна
        /// </summary>
        public int HeightDiff { get; set; }

        public int Height
        {
            get => height;
            set => SetProperty(ref height, value - HeightDiff);
        }

        public int Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }
    }
}
