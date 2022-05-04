namespace PTC_Management
{
    using PTC_Management.EF;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Employee")]
    public partial class Employee : Entity
    {
        static AppContext db;
        public Employee()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            Itinerary = new HashSet<Itinerary>();
        }


        [StringLength(50)]
        public string surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        public virtual ICollection<Date_has_Employee>
            Date_has_Employee { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
}