namespace PTC_Management.Model
{
    internal class Employee
    {
        public int idEmployee { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string driverLicence { get; set; }

        public static Employee[] GetInfo()
        {
            var info = new Employee[] {
                new Employee { idEmployee = 1 },
                new Employee { idEmployee = 2 },
                new Employee { idEmployee = 3 },
                new Employee { idEmployee = 4 },
                new Employee { idEmployee = 5 }
            };
            return info;
        }
    }


}
