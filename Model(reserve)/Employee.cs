using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Windows;

namespace PTC_Management.Model
{

    internal class Employee
    {
        [Key]
        public int idEmployee { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string driverLicence { get; set; }

        //public static Employee[] GetInfo()
        //{
        //    var info = new Employee[] {
        //        new Employee { idEmployee = 1 },
        //        new Employee { idEmployee = 2 },
        //        new Employee { idEmployee = 3 },
        //        new Employee { idEmployee = 4 },
        //        new Employee { idEmployee = 5 }
        //    };

        //    //var info = new Employee[10000000];
        //    //for (int i = 0; i < info.Length; i++) info[i] = new Employee { idEmployee = i };
        //    return info;
        //}


        public static ObservableCollection<Employee> GetInfo()
        {
            EmployeeContext db = new EmployeeContext();
            db.E.Load();
            db.E.Add(new Employee { surname = "5" });
            db.SaveChanges();

            return db.E.Local;
        }

    }

    internal class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<Employee> E { get; set; }
    }

}
