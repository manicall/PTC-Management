namespace PTC_Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Itinerary")]
    public partial class Itinerary
    {
        [Key]
        public int idItinerary { get; set; }

        public int Route_idRoute { get; set; }

        public int Transport_idTransport { get; set; }

        public int Employee_idEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Route Route { get; set; }

        public virtual Transport Transport { get; set; }
    }
}
