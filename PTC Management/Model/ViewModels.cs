using PTC_Management.ViewModel;

namespace PTC_Management.Model
{
    class ViewModels
    {
        public WindowParameters WindowParameters { get; set; }

        public ViewModels(WindowParameters windowParameters)
        {
            WindowParameters = windowParameters;
        }

        public T GetNewModelView<T>(int width) where T : ViewModelBaseEntity, new()
        {
            var viewModel = new T
            {
                WindowParameters = WindowParameters
            };
            viewModel.WindowParameters.WindowSize.Width = width;

            return viewModel;
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

        public ViewModelBaseEntity GetViewModel(string destination)
        {
            switch (destination)
            {
                case Destinations.employee: return GetNewModelView<EmployeeViewModel>(Size.defaultWidth);
                case Destinations.route: return GetNewModelView<RouteViewModel>(Size.defaultWidth);
                case Destinations.transport: return GetNewModelView<TransportViewModel>(Size.defaultWidth);
                case Destinations.itinerary: return GetNewModelView<ItineraryViewModel>(Size.defaultWidth);
                case Destinations.schedule: return GetNewModelView<ScheduleOfEmployeeViewModel>(1000);
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
