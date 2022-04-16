using PTC_Management.ViewModel;

namespace PTC_Management
{

    class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
        }

        private MaintanceLogViewModel maintanceLogViewModel = new MaintanceLogViewModel();
        private EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        private RouteViewModel routeViewModel = new RouteViewModel();
        private TransportViewModel transportViewModel = new TransportViewModel();
        private EmployeeScheduleViewModel employeeScheduleViewModel = new EmployeeScheduleViewModel();

        private BindableBase _CurrentViewModel;

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
                case "routes":
                    CurrentViewModel = routeViewModel;
                    break;
                case "employees":
                    CurrentViewModel = employeeViewModel;
                    break;
                 case "transport":
                    CurrentViewModel = transportViewModel;
                    break;
                 case "employeeSchedule":
                    CurrentViewModel = employeeScheduleViewModel;
                    break;
                default:
                    CurrentViewModel = null;
                    break;
            }
        }
    }
}