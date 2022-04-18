namespace PTC_Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transport")]
    public partial class Transport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        [Key]
        public int idTransport { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(9)]
        public string licensePlate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }
        public static Transport[] GetInfo()
        {
            var info = new Transport[] {
                new Transport { idTransport = 1 },
                new Transport { idTransport = 2 },
                new Transport { idTransport = 3 },
                new Transport { idTransport = 4 },
                new Transport { idTransport = 5 },
                new Transport { idTransport = 6 }
            };
            return info;
        }
    }

}
