using PTC_Management.Model;
using PTC_Management.Views.Windows;

namespace PTC_Management.ViewModel
{
    internal class TransportInfoWindowViewModel : ViewModelBaseWindow
    {
        // необходимо обязательно определить
        // конструктор без параметров,
        // так как его пытается найти метод
        // InitializeComponent() в классе TransportInfoWindow
        public TransportInfoWindowViewModel() { }

        public TransportInfoWindowViewModel(string destination, int idTransport)
        {
            switch (destination)
            {
                case Destinations.maintanceLog:
                    WindowParameters.WindowSize.Width = 900;
                    CurrentViewModel = viewModels.GetMaintanceLogVM(idTransport);
                    Title = "Журнал технического обслуживания";

                    break;

                case Destinations.logOfDepartureAndEntry:
                    CurrentViewModel = viewModels.GetLogOfDepartureAndEntryVM(idTransport);
                    Title = "Журнал регистрации въезда и выезда";

                    break;
            }
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
