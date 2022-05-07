namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Date_has_Employee
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEmployee { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDate { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public virtual Date Date { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
