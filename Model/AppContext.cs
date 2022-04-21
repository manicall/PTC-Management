using System.Data.Entity;

namespace PTC_Management
{
    public partial class AppContext : DbContext
    {
        public AppContext() : base("name=AppContext") { }

        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Date_has_Employee> Date_has_Employee { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeSchedule> EmployeeSchedule { get; set; }
        public virtual DbSet<Itinerary> Itinerary { get; set; }
        public virtual DbSet<LogOfDepartureAndEntry> 
            LogOfDepartureAndEntry { get; set; }
        public virtual DbSet<MaintanceLog> MaintanceLog { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Transport> Transport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Date>()
                .HasMany(e => e.Date_has_Employee)
                .WithRequired(e => e.Date)
                .HasForeignKey(e => e.Date_idDate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Date>()
                .HasMany(e => e.EmployeeSchedule)
                .WithMany(e => e.Date)
                .Map(m => m.ToTable("EmployeeSchedule_for_Month"));

            modelBuilder.Entity<Date_has_Employee>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.middleName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.driverLicense)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Date_has_Employee)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Employee_idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Employee_idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeSchedule>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Route)
                .HasForeignKey(e => e.Route_idRoute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Transport>()
                .Property(e => e.licensePlate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.Itinerary)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.Transport_idTransport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.LogOfDepartureAndEntry)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.Transport_idTransport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.MaintanceLog)
                .WithRequired(e => e.Transport)
                .HasForeignKey(e => e.Transport_idTransport)
                .WillCascadeOnDelete(false);
        }
    }
}
