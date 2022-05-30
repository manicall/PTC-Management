namespace PTC_Management.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogOfDepartureAndEntry")]
    public partial class LogOfDepartureAndEntry
    {
        public int Id { get; set; }

        public int IdTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan TimeOnDeparture { get; set; }

        public TimeSpan TimeWhenReturning { get; set; }

        public virtual Transport Transport { get; set; }
    }
}
