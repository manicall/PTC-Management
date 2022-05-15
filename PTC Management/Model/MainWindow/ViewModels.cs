using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;
using PTC_Management.Model.MainWindow;

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

    }

}
