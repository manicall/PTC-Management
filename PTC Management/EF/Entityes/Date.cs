namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Date")]
    public partial class Date
    {
        public int Id { get; set; }

        [Column("Date", TypeName = "date")]
        public DateTime Date1 { get; set; }

        [StringLength(25)]
        public string Status { get; set; }

        public int IdYearAndMonth { get; set; }

        public virtual YearAndMonth YearAndMonth { get; set; }
    }
}
