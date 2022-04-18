namespace PTC_Management
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        public static Route[] GetInfo()
        {
            var info = new Route[] {
                new Route { idRoute = 25, name="Дом творчества (66 квартал) - Завод \"Амурсталь\" (ул.Заводская)", distant=18.8f }
            };
            return info;
        }

        [Key]
        public int idRoute { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public float? distant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }
}
