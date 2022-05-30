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
        public Employee()
        {
            Date = new HashSet<Date>();
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(50)]
        public string DriverLicense { get; set; }

        public virtual ICollection<Date> Date { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
    public partial class Employee : Entity, IDataErrorInfo
    {
        public static readonly Repository<Employee> repository =
            new Repository<Employee>(new PTC_ManagementContext());

        // переопределение методов базового класса
        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

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
            return new Employee
            {
                Id = Id,
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                DriverLicense = DriverLicense
            };
        }

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода
        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Surname":
                        if (string.IsNullOrEmpty(Surname))
                            error = "Поле не может быть пустым";
                        else if (Surname.Contains(" "))
                            error = "Поле не должно содержать пробелы";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Поле не может быть пустым";
                        else if (Name.Contains(" "))
                            error = "Поле не должно содержать пробелы";
                        break;
                    case "Patronymic":
                        if (!string.IsNullOrEmpty(Patronymic)
                            && Patronymic.Contains(" "))
                            error = "Поле не должно содержать пробелы";
                        break;
                    case "DriverLicense":
                        if (string.IsNullOrEmpty(DriverLicense))
                            error = "Поле не может быть пустым";
                        else if (DriverLicense.Contains(" "))
                            error = "Поле не должно содержать пробелы";
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

