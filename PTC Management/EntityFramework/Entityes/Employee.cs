using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

using static System.Net.Mime.MediaTypeNames;

namespace PTC_Management.EF
{
    [Table("Employee")]
    public partial class Employee : Entity
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



    public partial class Employee : Entity
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
            var employee = new Employee { Id = Id };
            employee.SetFields(this);

            return employee;
        }

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName) { 
                        case "Surname":
                        error = GetNullOrNameError(Surname);
                        break;
                    case "Name":
                        error = GetNullOrNameError(Name);
                        break;
                    case "Patronymic":
                        error = GetPatronymicError(Patronymic);
                        break;
                    case "DriverLicense":
                        error = GetDriverLicenseError(DriverLicense);
                        break;
                } 
                return error;
            }
        }

        /// <summary>
        /// Проверяет, что поле содержит только буквы и дефисы
        /// </summary>
        string GetPatronymicError(string text)
        {
            if (string.IsNullOrEmpty(text)) return null; 
            if (Regex.IsMatch(text, "[^А-Яа-яA-Za-z-]+"))
                return "Поле может содержать только буквы и дефисы";
            if (text.Length > 50)
                return SizeError(50);
            return null;
        }

        /// <summary>
        /// Проверяет, что поле не является пустым и содержит только буквы и дефисы
        /// </summary>
        string GetNullOrNameError(string text) {
            if (string.IsNullOrEmpty(text))
                return "Поле не может быть пустым";
            if (Regex.IsMatch(text, "[^А-Яа-яA-Za-z-]+"))
                return "Поле может содержать только буквы и дефисы";
            if (text.Length > 50)
                return SizeError(50);
            return null;
        }

        /// <summary>
        /// Проверяет, что поле не является пустым и
        /// содержит только цифры при этом не более 10
        /// </summary>
        string GetDriverLicenseError(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "Поле не может быть пустым";
            if (Regex.IsMatch(text, "[\\D]+"))
                return "Поле может содержать только цифры";
            if (text.Length > 10) 
                return SizeError(10);
            return null;
        }

        string SizeError(int size) { return $"Максимальное количество символов в строке: {size}"; }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}

