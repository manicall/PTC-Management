using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.MainWindow
{
    internal class Size : BindableBase
    {
        private const int defaultWidth = 650;
        private const int defaultHeight = 500;

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
            set => height = value - 170;
        }

        private int width;
        public int Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }
    }
}
