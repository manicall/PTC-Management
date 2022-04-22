namespace PTC_Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Date")]
    public partial class Date
    {
        public Date()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            EmployeeSchedule = new HashSet<EmployeeSchedule>();
        }

        [Key]
        public int idDate { get; set; }

        [Column("date", TypeName = "date")]
        public DateTime? date1 { get; set; }

        public virtual ICollection<Date_has_Employee>
            Date_has_Employee { get; set; }
      
        public virtual ICollection<EmployeeSchedule>
            EmployeeSchedule { get; set; }
    }
}
