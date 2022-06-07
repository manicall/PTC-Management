using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Date")]
    public partial class Date
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Column("Date", TypeName = "date")]
        public DateTime Date1 { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public int IdEmployee { get; set; }

        public int IdLaborShift { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LaborShift LaborShift { get; set; }
    }
}
