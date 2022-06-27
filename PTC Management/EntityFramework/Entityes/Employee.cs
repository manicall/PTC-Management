using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PTC_Management.EntityFramework
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
        [StringLength(10)]
        public string DriverLicense { get; set; }

        public virtual ICollection<Date> Date { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
    public partial class Employee : Entity
    {
        public static readonly Repository<Employee> repository =
            new Repository<Employee>();

        // переопределение методов базового класса
        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

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

        public string GetFullName() => Surname + " " + Name + " " + Patronymic;

        public override Entity Clone() => Clone<Employee>();

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода

        // используется, чтобы поля не подсвечивались при открытии окна
        bool[] canExecute = new bool[3];

        public bool GetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                if (canExecute[i] != true) return false;
            }
            return true;
        }

        public void SetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                canExecute[i] = true;
            }

            // вызов события, на которое среагирует проверка валидации
            RaisePropertyChanged(nameof(Surname));
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(DriverLicense));
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Surname":
                        if (!string.IsNullOrEmpty(Surname))
                            canExecute[0] = true;

                        if (canExecute[0])
                            error = GetNullOrNameError(Surname);
                        break;
                    case "Name":
                        if (!string.IsNullOrEmpty(Name))
                            canExecute[1] = true;

                        if (canExecute[1])
                            error = GetNullOrNameError(Name);
                        break;
                    case "Patronymic":
                        error = GetPatronymicError(Patronymic);
                        break;
                    case "DriverLicense":
                        if (!string.IsNullOrEmpty(Name))
                            canExecute[2] = true;

                        if (canExecute[2])
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
            if (text != null && Regex.IsMatch(text, "[^А-Яа-яA-Za-z-]+"))
                return "Поле может содержать только буквы и дефисы";
            return null;
        }

        /// <summary>
        /// Проверяет, что поле не является пустым и содержит только буквы и дефисы
        /// </summary>
        string GetNullOrNameError(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "Поле не может быть пустым";
            if (text != null && Regex.IsMatch(text, "[^А-Яа-яA-Za-z-]+"))
                return "Поле может содержать только буквы и дефисы";
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
            if (text != null && Regex.IsMatch(text, "[\\D]+"))
                return "Поле может содержать только цифры";
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}

