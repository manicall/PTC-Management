namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route : Entity
    {
        
        public Route()
        {
            Itineraries = new HashSet<Itinerary>();
        }

        public int Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float? Distant { get; set; }

        
        public virtual ICollection<Itinerary> Itineraries { get; set; }
    }
}
