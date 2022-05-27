using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels : BindableBase
    {
        public readonly ViewModelBaseEntity employee;
        public readonly ViewModelBaseEntity route;
        public readonly ViewModelBaseEntity transport;
        public readonly ViewModelBaseEntity itinerary;
        public readonly ViewModelBaseEntity scheduleOfEmployee;

        public ViewModels(Size mainWindowSize)
        {
            employee = new EmployeeViewModel()
            {
                MainWidowSize = mainWindowSize
            };

            route = new RouteViewModel()
            {
                MainWidowSize = mainWindowSize
            };

            transport = new TransportViewModel()
            {
                MainWidowSize = mainWindowSize
            };

            itinerary = new ItineraryViewModel();
            scheduleOfEmployee = new ScheduleOfEmployeeViewModel();
        }

        public ViewModelBaseEntity GetViewModel(string destination) {
            switch (destination)
            {
                case Destinations.employee:  return employee;
                case Destinations.route:     return route;
                case Destinations.transport: return transport;
                case Destinations.itinerary: return itinerary;
                case Destinations.schedule : return scheduleOfEmployee;
                default: return null;
            }
        }

    }

}
