using System.Data.Entity;

namespace PTC_Management.EntityFramework
{
    public partial class AppContext : DbContext
    {
        public AppContext()
            : base("name=PTC_ManagementConnection") { }

        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Itinerary> Itinerary { get; set; }
        public virtual DbSet<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }
        public virtual DbSet<MaintanceLog> MaintanceLog { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Transport> Transport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Date>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Date)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.IdEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.IdEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Itinerary>()
                .HasMany(e => e.LogOfDepartureAndEntry)
                .WithRequired(e => e.Itinerary)
                .HasForeignKey(e => e.IdItinerary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Itinerary>()
                .HasMany(e => e.MaintanceLog)
                .WithRequired(e => e.Itinerary)
                .HasForeignKey(e => e.IdItinerary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaintanceLog>()
                .Property(e => e.MaintenanceType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.Distant)
                .HasPrecision(4, 1);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Route)
                .HasForeignKey(e => e.IdRoute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.IdTransport)
                .WillCascadeOnDelete(false);
        }
    }

}
