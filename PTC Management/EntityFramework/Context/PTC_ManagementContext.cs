using System.Data.Entity;

namespace PTC_Management.EF
{

    public partial class PTC_ManagementContext : DbContext
    {
        public PTC_ManagementContext()
            : base("name=PTC_ManagementConnection") { }

        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Itinerary> Itinerary { get; set; }
        public virtual DbSet<LaborShift> LaborShift { get; set; }
        public virtual DbSet<LogOfDepartureAndEntry> LogOfDepartureAndEntry 
        { 
            get; set;
        }
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
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.DriverLicense)
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

            modelBuilder.Entity<LaborShift>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LaborShift>()
                .HasMany(e => e.Date)
                .WithRequired(e => e.LaborShift)
                .HasForeignKey(e => e.IdLaborShift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Route)
                .HasForeignKey(e => e.IdRoute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Transport>()
                .Property(e => e.LicensePlate)
                .IsUnicode(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.IdTransport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.LogOfDepartureAndEntry)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.IdTransport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.MaintanceLog)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.IdTransport)
                .WillCascadeOnDelete(false);
        }
    }
}
