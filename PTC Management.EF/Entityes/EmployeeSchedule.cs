namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeSchedule")]
    public partial class EmployeeSchedule : Entity
    {
        
        public EmployeeSchedule()
        {
            Dates = new HashSet<Date>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        
        public virtual ICollection<Date> Dates { get; set; }
    }
}
