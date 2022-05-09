using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels : BindableBase
    {
        public readonly BindableBase employee;
        public readonly BindableBase route;
        public readonly BindableBase transport;
        public readonly BindableBase itinerary;
        public readonly BindableBase scheduleOfEmployee;

        public ViewModels(Size mainWindowSize)
        {
            employee = new EmployeeViewModel() { MainWidowSize = mainWindowSize };
            route = new RouteViewModel();
            transport = new TransportViewModel();
            itinerary = new ItineraryViewModel();
            scheduleOfEmployee = new ScheduleOfEmployeeViewModel();
        }

    }

}
