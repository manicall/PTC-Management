namespace PTC_Management.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaintanceLog")]
    public partial class MaintanceLog
    {
        public int Id { get; set; }

        public int IdTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

        public int? SpeedometerInfoOnDeparture { get; set; }

        public int? SpeedometerInfoWhenReturning { get; set; }

        public int? Mileage { get; set; }

        public virtual Transport Transport { get; set; }
    }
}
