using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    internal class Size : BindableBase
    {
        public const int defaultWidth = 700;
        public const int defaultHeight = 500;

        public Size(
            int width = defaultWidth,
            int height = defaultHeight)
        {
            this.width = width;
            this.height = height;
        }

        private int height;
        public int Height
        {
            get => height;
            // вычитание для того, чтобы размер таблицы подстраивался под размер окна
            set => SetProperty(ref height, value - 170);
        }

        private int width;
        public int Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }
    }
}
