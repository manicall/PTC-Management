using PTC_Management.EF;
using PTC_Management.ViewModel.Base;
using PTC_Management.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.ViewModel
{
    internal class TransportInfoViewModel : ViewModelBaseWindow
    {

        private Transport selectedTransport;

        public Transport SelectedTransport
        {
            get { return selectedTransport; }
            set { SetProperty(ref selectedTransport, value); }
        }


        /// <summary> Метод показа ViewModel в окне </summary>
        public void Show()
        {
            window = new TransportInfoWindow();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }

    }
}
