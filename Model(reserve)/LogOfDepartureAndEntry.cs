using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    public class LogOfDepartureAndEntry
    {
        public int idLogOfDepartureAndEntry { get; set; }
        public DateTime date { get; set; }
        public int idList { get; set; }
        public string transport { get; set; }
        public string licensePlate { get; set; }
        public DateTime timeOnDeparture { get; set; }
        public DateTime timeWhenReturning { get; set; }

        public int mileage { get; set; }

        public static LogOfDepartureAndEntry[] GetInfo()
        {
            var info = new LogOfDepartureAndEntry[] {
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 1, date = DateTime.Parse("01.12.2020"), idList = 1269, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 2, date = DateTime.Parse("02.12.2020"), idList = 1271, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 3, date = DateTime.Parse("03.12.2020"), idList = 1274, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 4, date = DateTime.Parse("06.12.2020"), idList = 1284, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 5, date = DateTime.Parse("07.12.2020"), idList = 1287, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 5, date = DateTime.Parse("07.12.2020"), idList = 1287, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 5, date = DateTime.Parse("07.12.2020"), idList = 1287, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
                new LogOfDepartureAndEntry { idLogOfDepartureAndEntry = 5, date = DateTime.Parse("07.12.2020"), idList = 1287, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00")},
            };
            return info;
        }

    }
}
