using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    internal class ItineraryLog
    {
        public int idItineraryLog { get; set; }

        public Employee employee { get; set; }


        public static ItineraryLog[] GetInfo()
        {
            var info = new ItineraryLog[] {
                new ItineraryLog { idItineraryLog=1, employee = new Employee { idEmployee = 1, surname="Иван" } },
            };
            return info;
        }

    }
}
