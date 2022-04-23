namespace PTC_Management
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Route")]
    public partial class Route
    {
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
        public string Name { get; set; }

        public float? distant { get; set; }
   
        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }
}
