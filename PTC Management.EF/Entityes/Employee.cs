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

        public static readonly Repository<Employee> repositoryEmployee = 
            new Repository<Employee>(new PTC_ManagementContext());

        public override void Add() { repositoryEmployee.Add(this);  }
        public override void Update() {
            var employee = repositoryEmployee.GetSingle(Id);
            employee.SetFields(this);
            repositoryEmployee.Update(employee); 
        }
        public override void Remove() { repositoryEmployee.Remove(this); }
        public override void Copy(int count) {
            var employee = new Employee();
            employee.SetFields(repositoryEmployee.GetSingle(Id)); 
            repositoryEmployee.Copy(employee, count); }

        public void SetFields(Employee employee)
        {
            Surname = employee.Surname;
            Name = employee.Name;
            Patronymic = employee.Patronymic;
            DriverLicense = employee.DriverLicense;
        }

        public override Entity Clone() {
            Employee employee = new Employee();

            employee.Surname = Surname;
            employee.Name = Name;
            employee.Patronymic = Patronymic;
            employee.DriverLicense = DriverLicense;
            return employee;
        }
    }
}
