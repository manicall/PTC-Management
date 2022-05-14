using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model.MainWindow
{
    internal class Size
    {
        public Size(int height) { this.height = height; }
        private int height;
        public int Height
        {
            get => height;
            set => height = value - 170;
        }
    }
}
