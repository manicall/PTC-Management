using PTC_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels : BindableBase
    {
        public readonly BindableBase employee;
        public readonly BindableBase route;
        public readonly BindableBase transport;
        public readonly BindableBase itinerary;
        public readonly BindableBase scheduleOfEmployee;

        public ViewModels(ref int MainWindowHeight)
        {
            employee = new EmployeeViewModel();
            route = new RouteViewModel();
            transport = new TransportViewModel();
            itinerary = new ItineraryViewModel();
            scheduleOfEmployee = new ScheduleOfEmployeeViewModel();
        }

    }

}
