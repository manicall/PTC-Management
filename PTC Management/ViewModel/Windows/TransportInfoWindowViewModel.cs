using PTC_Management.ViewModel.Base;
using PTC_Management.Views.Windows;

namespace PTC_Management.ViewModel
{
    internal class TransportInfoWindowViewModel : ViewModelBaseWindow
    {

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
