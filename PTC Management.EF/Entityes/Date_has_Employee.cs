namespace PTC_Management
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Date_has_Employee
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Employee_IdEmployee { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Date_IdDate { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public virtual Date Date { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
