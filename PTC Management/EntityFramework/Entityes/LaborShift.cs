namespace PTC_Management.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LaborShift")]
    public partial class LaborShift
    {
        public LaborShift()
        {
            Date = new HashSet<Date>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        
        public virtual ICollection<Date> Date { get; set; }
    }
}
