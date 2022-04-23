namespace PTC_Management
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Itinerary")]
    public partial class Itinerary
    {
        [Key]
        public int idItinerary { get; set; }

        public int Route_idRoute { get; set; }

        public int Transport_idTransport { get; set; }

        public int Employee_IdEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Route Route { get; set; }

        public virtual Transport Transport { get; set; }

        public static ObservableCollection<Itinerary> GetInfo()
        {
            AppContext db = new AppContext();
            db.Itinerary.Load();

            return db.Itinerary.Local;
        }
    }
}
