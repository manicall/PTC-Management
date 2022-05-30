using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

using System;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels : BindableBase
    {
        public readonly ViewModelBaseEntity employee;
        public readonly ViewModelBaseEntity route;
        public readonly ViewModelBaseEntity transport;
        public readonly ViewModelBaseEntity itinerary;
        public readonly ViewModelBaseEntity scheduleOfEmployee;
        public readonly ViewModelBaseEntity maintanceLog;
        public readonly ViewModelBaseEntity logOfDepartureAndEntry;

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
            maintanceLog = new MaintanceLogViewModel();
            logOfDepartureAndEntry = new LogOfDepartureAndEntryViewModel();
        }

        public ViewModelBaseEntity GetViewModel(string destination) {
            switch (destination)
            {
                case Destinations.employee:  return employee;
                case Destinations.route:     return route;
                case Destinations.transport: return transport;
                case Destinations.itinerary: return itinerary;
                case Destinations.schedule : return scheduleOfEmployee;
                case Destinations.maintanceLog: return maintanceLog;
                case Destinations.logOfDepartureAndEntry : return logOfDepartureAndEntry;
                default: return null;
            }
        }

        public static string GetDialogTitle(string action, string destination)
        {
            switch (destination)
            { 
                case Destinations.employee: 
                    return Actions.GetGenetiveName(action) + " сотрудника";
                case Destinations.route: 
                    return Actions.GetGenetiveName(action) + " маршрута";
                case Destinations.transport: 
                    return Actions.GetGenetiveName(action) + " транспорта";
                case Destinations.itinerary: 
                    return Actions.GetGenetiveName(action) + " путевого листа";
                case Destinations.schedule: 
                    return Actions.GetGenetiveName(action) + " расписания";
                case Destinations.maintanceLog: 
                    return Actions.GetGenetiveName(action) + " записи технического обслуживания";
                case Destinations.logOfDepartureAndEntry: 
                    return Actions.GetGenetiveName(action) + " записи въезда и выезда";
                default: return null;
            }
        }
    }

}
