namespace PTC_Management
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MaintanceLog")]
    public partial class MaintanceLog
    {
        [Key]
        [Column(Order = 0)]
        public int idMaintanceLog { get; set; }

        internal static object GetInfo()
        {
            throw new NotImplementedException();
        }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Transport_idTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public TimeSpan? timeOnDeparture { get; set; }

        public TimeSpan? timeWhenReturning { get; set; }

        public int? speedometerInfoOnDeparture { get; set; }

        public int? speedometerInfoWhenReturning { get; set; }

        public int? mileage { get; set; }

        public virtual Transport Transport { get; set; }
    }
}
