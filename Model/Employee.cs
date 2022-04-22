namespace PTC_Management
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Employee")]
    public partial class Employee
    {
        public Employee()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            Itinerary = new HashSet<Itinerary>();
        }

        [Key]
        public int idEmployee { get; set; }

        [StringLength(50)]
        public string surname { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string middleName { get; set; }

        [StringLength(50)]
        public string driverLicense { get; set; }

        public virtual ICollection<Date_has_Employee>
            Date_has_Employee { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

        public static ObservableCollection<Employee> GetInfo()
        {
            AppContext db = new AppContext();
            db.Employee.Load();

            return db.Employee.Local;
        }
    }
}
