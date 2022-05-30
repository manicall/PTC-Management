namespace PTC_Management.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Date")]
    public partial class Date
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Date", TypeName = "date")]
        public DateTime Date1 { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public int IdEmployee { get; set; }

        public int IdLaborShift { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LaborShift LaborShift { get; set; }
    }
}
