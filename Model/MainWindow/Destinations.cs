using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model.MainWindow
{
    public class Destinations
    {
        public const string _employee = "employee";
        public const string _routes = "routes";
        public const string _transport = "transport";
        public const string _itinerary = "itinerary";
        public const string _schedule = "schedule";

        public string Employee => _employee;
        public string Routes => _routes;
        public string Transport => _transport;
        public string Itinerary => _itinerary;
        public string Schedule => _schedule;
    }
}
