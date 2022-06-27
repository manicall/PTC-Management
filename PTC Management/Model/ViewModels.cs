using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    public class ViewModels
    {
        public WindowParameters WindowParameters { get; set; }
        
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
                    return GetModelView<EmployeeViewModel>();
                case Destinations.route:
                    return GetModelView<RouteViewModel>();
                case Destinations.transport:
                    return GetModelView<TransportViewModel>();
                case Destinations.itinerary:
                    return GetModelView<ItineraryViewModel>();
                case Destinations.schedule:
                    return GetModelView<ScheduleOfEmployeeViewModel>();
                default: 
                    return null;
            }
        }

        /// <summary>
        /// Универсальный метод для возврата модели представления 
        /// </summary>
        public T GetModelView<T>() where T : ViewModelBaseEntity, new()
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
