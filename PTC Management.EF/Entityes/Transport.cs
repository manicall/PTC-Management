namespace PTC_Management
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("Transport")]
    public partial class Transport
    {
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        [Key]
        public int idTransport { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(9)]
        public string licensePlate { get; set; }
  
        public virtual ICollection<Itinerary> Itinerary { get; set; }
        
        public virtual ICollection<LogOfDepartureAndEntry>
            LogOfDepartureAndEntry { get; set; }
     
        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }
        public static ObservableCollection<Transport> GetInfo()
        {
            AppContext db = new AppContext();
            db.Transport.Load();

            return db.Transport.Local;
        }
    }

}
