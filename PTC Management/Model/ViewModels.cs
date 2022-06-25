using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    public class ViewModels
    {
        public WindowParameters WindowParameters { get; set; }
        
        private EmployeeViewModel employeeVM;
        private RouteViewModel routeVM;
        private TransportViewModel transportVM ;
        private ItineraryViewModel itineraryVM;
        private ScheduleOfEmployeeViewModel scheduleOfEmployeeVM;

        public EmployeeViewModel EmployeeVM { get => employeeVM ?? (employeeVM = GetNewModelView<EmployeeViewModel>()); }
        public RouteViewModel RouteVM { get => routeVM ?? (routeVM = GetNewModelView<RouteViewModel>()); }
        public TransportViewModel TransportVM { get => transportVM ?? (transportVM = GetNewModelView<TransportViewModel>()); }
        public ItineraryViewModel ItineraryVM { get => itineraryVM ?? (itineraryVM = GetNewModelView<ItineraryViewModel>()); }
        public ScheduleOfEmployeeViewModel ScheduleOfEmployeeVM { get => scheduleOfEmployeeVM ?? (scheduleOfEmployeeVM = GetNewModelView<ScheduleOfEmployeeViewModel>()); }

        public ViewModels(WindowParameters windowParameters)
        {
            WindowParameters = windowParameters;
        }

        public ItineraryViewModel GetItineraryVM(int idTransport)
        {
            return new ItineraryViewModel(idTransport) { WindowParameters = WindowParameters };
        }

        public MaintanceLogViewModel GetMaintanceLogVM(int idTransport)
        {
            return new MaintanceLogViewModel(idTransport) { WindowParameters = WindowParameters };
        }

        public LogOfDepartureAndEntryViewModel GetLogOfDepartureAndEntryVM(int idTransport)
        {
            return new LogOfDepartureAndEntryViewModel(idTransport) { WindowParameters = WindowParameters };
        }

        /// <summary>
        /// Возвращает модель представления 
        /// на основе заданного параметра
        /// </summary>
        public ViewModelBaseEntity GetViewModel(string destination)
        {
            switch (destination)
            {
                case Destinations.employee:
                    return EmployeeVM;
                case Destinations.route:
                    return RouteVM;
                case Destinations.transport:
                    return TransportVM;
                case Destinations.itinerary:
                    return ItineraryVM;
                case Destinations.schedule:
                    return ScheduleOfEmployeeVM;
                default: 
                    return null;
            }
        }

        /// <summary>
        /// Универсальный метод для возврата модели представления 
        /// </summary>
        public T GetNewModelView<T>() where T : ViewModelBaseEntity, new()
        {
            return new T
            {
                WindowParameters = WindowParameters
            };
        }

        /// <summary>
        /// Возвращает заголовок окна
        /// </summary>
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
                default:
                    return null;
            }
        }
    }
}
