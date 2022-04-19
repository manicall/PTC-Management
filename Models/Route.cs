namespace PTC_Management
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        public static ObservableCollection<Route> GetInfo()
        {
            AppContext db = new AppContext();
            db.Route.Load();

            return db.Route.Local;
        }

        [Key]
        public int idRoute { get; set; }

        public int number { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public float? distant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }
}
