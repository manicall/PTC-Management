namespace PTC_Management
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeSchedule")]
    public partial class EmployeeSchedule
    {
        public EmployeeSchedule()
        {
            Date = new HashSet<Date>();
        }

        [Key]
        public int IdEmployeeSchedule { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    
        public virtual ICollection<Date> Date { get; set; }
    }
}
