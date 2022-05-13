using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;

namespace PTC_Management.EF
{
    //public class MyDbConfiguration : DbConfiguration
    //{
    //    public MyDbConfiguration() : base()
    //    {
    //        var path = Path.GetDirectoryName(GetType().Assembly.Location);
    //        SetModelStore(new DefaultDbModelStore(path));
    //    }
    //}

    //[DbConfigurationType(typeof(MyDbConfiguration))]
    public partial class PTC_ManagementContext : DbContext
    {
        public PTC_ManagementContext()
            : base("name=PTC_ManagementConnection")
        {

        }

        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<Date_has_Employee> Date_has_Employee { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<LogOfDepartureAndEntry> LogOfDepartureAndEntries { get; set; }
        public virtual DbSet<MaintanceLog> MaintanceLogs { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Date>()
        //        .HasMany(e => e.Date_has_Employee)
        //        .WithRequired(e => e.Date)
        //        .HasForeignKey(e => e.IdDate)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Date>()
        //        .HasMany(e => e.EmployeeSchedule)
        //        .WithMany(e => e.Date)
        //        .Map(m => m.ToTable("EmployeeSchedule_for_Month").MapLeftKey("IdDate").MapRightKey("IdEmployeeSchedule"));

        //    modelBuilder.Entity<Date_has_Employee>()
        //        .Property(e => e.Status)
        //        .IsFixedLength()
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Employee>()
        //        .Property(e => e.Surname)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Employee>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Employee>()
        //        .Property(e => e.Patronymic)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Employee>()
        //        .Property(e => e.DriverLicense)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Employee>()
        //        .HasMany(e => e.Date_has_Employee)
        //        .WithRequired(e => e.Employee)
        //        .HasForeignKey(e => e.IdEmployee)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Employee>()
        //        .HasMany(e => e.Itinerary)
        //        .WithRequired(e => e.Employee)
        //        .HasForeignKey(e => e.IdEmployee)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<EmployeeSchedule>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Route>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Route>()
        //        .HasMany(e => e.Itinerary)
        //        .WithRequired(e => e.Route)
        //        .HasForeignKey(e => e.IdRoute)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Transport>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Transport>()
        //        .Property(e => e.LicensePlate)
        //        .IsFixedLength()
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Transport>()
        //        .HasMany(e => e.Itinerary)
        //        .WithRequired(e => e.Transport)
        //        .HasForeignKey(e => e.IdTransport)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Transport>()
        //        .HasMany(e => e.LogOfDepartureAndEntry)
        //        .WithRequired(e => e.Transport)
        //        .HasForeignKey(e => e.IdTransport)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Transport>()
        //        .HasMany(e => e.MaintanceLog)
        //        .WithRequired(e => e.Transport)
        //        .HasForeignKey(e => e.IdTransport)
        //        .WillCascadeOnDelete(false);
        //}
    }
}
