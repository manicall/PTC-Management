namespace PTC_Management.EF
{


    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Employee")]
    public partial class Employee : Entity, IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Itinerary = new HashSet<Itinerary>();
            YearAndMonth = new HashSet<YearAndMonth>();
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
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YearAndMonth> YearAndMonth { get; set; }


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

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Surname":
                        if (string.IsNullOrEmpty(Surname))
                            error = "ѕоле с фамилией не может быть пустым";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "ѕоле с именем не может быть пустым";
                        break;
                    case "Patronymic":
                       
                        break;
                    case "DriverLicense":
                        if (string.IsNullOrEmpty(DriverLicense))
                            error = "ѕоле с водительским удостоверением" +
                                    " не может быть пустым";
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
