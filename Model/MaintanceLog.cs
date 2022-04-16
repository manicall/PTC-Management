using System;

namespace PTC_Management.Model
{

    public class MaintanceLog
    {
        public int idMaintanceLog { get; set; }
        public DateTime date { get; set; }
        public int idList { get; set; }
        public string transport { get; set; }
        public string licensePlate { get; set; }
        public string route { get; set; }
        public DateTime timeOnDeparture { get; set; }
        public DateTime timeWhenReturning { get; set; }
        public int speedometerInfoOnDeparture { get; set; }
        public int speedometerInfoWhenReturning { get; set; }
        public int mileage { get; set; }

        public static MaintanceLog[] GetInfo() {
            var info = new MaintanceLog[] {
                new MaintanceLog { idMaintanceLog = 1, date = DateTime.Parse("01.12.2020"), idList = 1269, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", route = "№ 25(Дом творчества \"66 кв.\" - Завод  Амурсталь \"Заводская\")", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00"), speedometerInfoOnDeparture = 385673, speedometerInfoWhenReturning = 385877, mileage = 204 },
                new MaintanceLog { idMaintanceLog = 2, date = DateTime.Parse("02.12.2020"), idList = 1271, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", route = "№ 25(Дом творчества \"66 кв.\" - Завод  Амурсталь \"Заводская\")", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00"), speedometerInfoOnDeparture = 385877, speedometerInfoWhenReturning = 386076, mileage = 199 },
                new MaintanceLog { idMaintanceLog = 3, date = DateTime.Parse("03.12.2020"), idList = 1274, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", route = "№ 25(Дом творчества \"66 кв.\" - Завод  Амурсталь \"Заводская\")", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00"), speedometerInfoOnDeparture = 386076, speedometerInfoWhenReturning = 386278, mileage = 202 },
                new MaintanceLog { idMaintanceLog = 4, date = DateTime.Parse("06.12.2020"), idList = 1284, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", route = "№ 25(Дом творчества \"66 кв.\" - Завод  Амурсталь \"Заводская\")", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00"), speedometerInfoOnDeparture = 386278, speedometerInfoWhenReturning = 386476, mileage = 198 },
                new MaintanceLog { idMaintanceLog = 5, date = DateTime.Parse("07.12.2020"), idList = 1287, transport = "Hyundai Aero City 540", licensePlate = "А336ТО 27", route = "№ 25(Дом творчества \"66 кв.\" - Завод  Амурсталь \"Заводская\")", timeOnDeparture = DateTime.Parse("06:00"), timeWhenReturning = DateTime.Parse("20:00"), speedometerInfoOnDeparture = 386476, speedometerInfoWhenReturning = 386682, mileage = 206 },
            };
            return info;
        }

    }

}
