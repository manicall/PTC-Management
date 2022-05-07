namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee : Entity
    {    
        public Employee()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            Itineraries = new HashSet<Itinerary>();
        }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        public virtual ICollection<Date_has_Employee> Date_has_Employee { get; set; }
        
        public virtual ICollection<Itinerary> Itineraries { get; set; }

        Repository<Employee> _employee = new Repository<Employee>(new PTC_ManagementContext());

        public override void Add() { _employee.Add(this);  }
        public override void Update() {
            var employee = _employee.Get(this.Id);
            employee.SetFields(this);
            _employee.Update(employee); 
        }
        public override void Remove() { _employee.Remove(this); }
        public override void Copy(int count) {
            var employee = _employee.Get(this.Id);
            employee.SetFields(this); 
            _employee.Copy(employee, count); }

        public void SetFields(Employee employee)
        {
            Id = employee.Id;
            Surname = employee.Surname;
            Name = employee.Name;
            Patronymic = employee.Patronymic;
            DriverLicense = employee.DriverLicense;
        }
    }
}
