namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Date")]
    public partial class Date : Entity
    {
        
        public Date()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            EmployeeSchedules = new HashSet<EmployeeSchedule>();
        }

        [Column("Date", TypeName = "date")]
        public DateTime? Date1 { get; set; }

        
        public virtual ICollection<Date_has_Employee> Date_has_Employee { 
            get; set; 
        }

        
        public virtual ICollection<EmployeeSchedule> EmployeeSchedules {
            get; set;
        }
    }
}
