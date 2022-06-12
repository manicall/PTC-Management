namespace PTC_Management.Model
{
    public class Destinations
    {
        public const string employee = "Employee";
        public const string route = "Route";
        public const string transport = "Transport";
        public const string itinerary = "Itinerary";
        public const string schedule = "Schedule";
        public const string maintanceLog = "MaintanceLog";
        public const string logOfDepartureAndEntry = "LogOfDepartureAndEntry";

        public string Employee => employee;
        public string Route => route;
        public string Transport => transport;
        public string Itinerary => itinerary;
        public string Schedule => schedule;
        public string MaintanceLog => maintanceLog;
        public string LogOfDepartureAndEntry => logOfDepartureAndEntry;

    }
}
