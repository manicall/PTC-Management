using PTC_Management.EF;
using PTC_Management.ViewModel;
using PTC_Management.ViewModel.Base;

namespace PTC_Management.Model.MainWindow
{
    class ViewModels
    {
        public WindowParameters WindowParameters { get; set; }

        public ViewModels(WindowParameters windowParameters)
        {
            WindowParameters = windowParameters;
        }

        public T GetNewModelView<T>() where T : ViewModelBaseEntity, new()
        {
            return new T() 
            {
               WindowParameters = WindowParameters
            };
        }

        public MaintanceLogViewModel GetMaintanceLogVM(Transport transport) 
        {
            return new MaintanceLogViewModel(transport) { WindowParameters = WindowParameters };
        }
        
        public LogOfDepartureAndEntryViewModel GetLogOfDepartureAndEntryVM(Transport transport) 
        {
            return new LogOfDepartureAndEntryViewModel(transport) { WindowParameters = WindowParameters };
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

        public static string GetTitle(string partOfTitle, string destination)
        {
            switch (destination)
            {
                case Destinations.employee:
                    return partOfTitle + " сотрудника";
                case Destinations.route:
                    return partOfTitle + " маршрута";
                case Destinations.transport:
                    return partOfTitle + " транспорта";
                case Destinations.itinerary:
                    return partOfTitle + " путевого листа";
                case Destinations.schedule:
                    return partOfTitle + " расписания";
                case Destinations.maintanceLog:
                    return partOfTitle + " записи технического обслуживания";
                case Destinations.logOfDepartureAndEntry:
                    return partOfTitle + " записи въезда и выезда";
                default: return null;
            }
        }
    }

}
