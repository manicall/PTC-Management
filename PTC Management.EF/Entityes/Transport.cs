namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transport")]
    public partial class Transport : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transport()
        {
            Itineraries = new HashSet<Itinerary>();
            LogOfDepartureAndEntries = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLogs = new HashSet<MaintanceLog>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(9)]
        public string LicensePlate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itineraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintanceLog> MaintanceLogs { get; set; }
    }
}
