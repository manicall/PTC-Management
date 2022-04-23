using PTC_Management.ViewModel;

namespace PTC_Management
{

    class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            // создаем команду перехватывающую сообщения от кнопки
            NavCommand = new ParameterizedCommand<string>(OnNav);
            // установка представления по умолчанию
            CurrentViewModel = employeeViewModel;
        }

        private BindableBase _CurrentViewModel;

        private EmployeeViewModel  employeeViewModel  = new EmployeeViewModel();
        private RouteViewModel     routeViewModel     = new RouteViewModel();
        private TransportViewModel transportViewModel = new TransportViewModel();
        private ItineraryViewModel ItineraryViewModel = new ItineraryViewModel();

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public ParameterizedCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {

            switch (destination)
            {
                case "employees":
                    CurrentViewModel = employeeViewModel;
                    break;
                case "routes":
                    CurrentViewModel = routeViewModel;
                    break;
                case "transport":
                    CurrentViewModel = transportViewModel;
                    break;
                case "itinerary":
                    CurrentViewModel = ItineraryViewModel;
                    break;
                default:
                    CurrentViewModel = null;
                    break;
            }
        }
    }
}