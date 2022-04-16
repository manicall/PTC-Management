using PTC_Management.ViewModel;

namespace PTC_Management
{

    class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = employeeViewModel;
        }

        private BindableBase _CurrentViewModel;

        private EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        private RouteViewModel routeViewModel = new RouteViewModel();
        private TransportViewModel transportViewModel = new TransportViewModel();
        private ItineraryLogViewModel itineraryLogViewModel = new ItineraryLogViewModel();

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public MyICommand<string> NavCommand { get; private set; }

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
                 case "ItineraryLog":
                    CurrentViewModel = itineraryLogViewModel;
                    break;
                default:
                    CurrentViewModel = null;
                    break;
            }
        }
    }
}