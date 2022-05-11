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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Date_has_Employee = new HashSet<Date_has_Employee>();
            Itinerary = new HashSet<Itinerary>();
        }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Date_has_Employee> Date_has_Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        public static readonly Repository<Employee> repository =
            new Repository<Employee>(new PTC_ManagementContext());

        public override void Add()
        {
            repository.Add(this);
        }

        public override void Update()
        {
            repository.Update(this);
        }

        public override void Remove()
        {
            repository.Remove(this);
        }

        public override void Copy(int count)
        {
            repository.Copy(this, count);
        }

        public override void SetFields(Entity entity)
        {
            if (entity is Employee employee)
            {
                Surname = employee.Surname;
                Name = employee.Name;
                Patronymic = employee.Patronymic;
                DriverLicense = employee.DriverLicense;
            }
        }

        public override Entity Clone()
        {
            Employee employee = new Employee
            {
                Id = Id,
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                DriverLicense = DriverLicense
            };
            return employee;
        }
    }
}
