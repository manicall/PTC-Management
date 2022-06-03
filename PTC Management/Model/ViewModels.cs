using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels
    {
        public WindowParameters WindowParameters { get; set; }

        public readonly ViewModelBaseEntity scheduleOfEmployee;

        public ViewModels(WindowParameters windowParameters)
        {
            WindowParameters = windowParameters;

            //scheduleOfEmployee = new ScheduleOfEmployeeViewModel();
        }

        public T GetNewModelView<T>() where T : ViewModelBaseEntity, new()
        {
            return new T() 
            {
               WindowParameters = WindowParameters
            };
        }

        public ViewModelBaseEntity GetViewModel(string destination)
        {
            switch (destination)
            {
                case Destinations.employee: return GetNewModelView<EmployeeViewModel>();
                case Destinations.route: return GetNewModelView<RouteViewModel>();
                case Destinations.transport: return GetNewModelView<TransportViewModel>();
                case Destinations.itinerary: return GetNewModelView<ItineraryViewModel>();
                //case Destinations.schedule: return scheduleOfEmployee;
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
