namespace PTC_Management
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LogOfDepartureAndEntry")]
    public partial class LogOfDepartureAndEntry
    {
        [Key]
        [Column(Order = 0)]
        public int idLogOfDepartureAndEntry { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Transport_idTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public TimeSpan? timeOnDeparture { get; set; }

        public TimeSpan? timeWhenReturning { get; set; }

        public virtual Transport Transport { get; set; }
    }
}
