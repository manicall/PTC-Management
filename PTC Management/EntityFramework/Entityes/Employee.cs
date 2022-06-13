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
        private string surname;

        public Employee()
        {
            Date = new HashSet<Date>();
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        [StringLength(50)]
        public string Surname { get => surname; set => SetProperty(ref surname, value); }

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
            new Repository<Employee>(new PTC_ManagementContext());

        // ��������������� ������� �������� ������
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

        public override Entity Clone() => Clone<Employee>();

        // ���������� ���������� IDataErrorInfo
        // ��������� ������������ ������,
        // ����������� � ����� ��� �����
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
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
        /// ���������, ��� ���� �������� ������ ����� � ������
        /// </summary>
        string GetPatronymicError(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (Regex.IsMatch(text, "[^�-��-�A-Za-z-]+"))
                return "���� ����� ��������� ������ ����� � ������";
            return null;
        }

        /// <summary>
        /// ���������, ��� ���� �� �������� ������ � �������� ������ ����� � ������
        /// </summary>
        string GetNullOrNameError(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "���� �� ����� ���� ������";
            if (Regex.IsMatch(text, "[^�-��-�A-Za-z-]+"))
                return "���� ����� ��������� ������ ����� � ������";
            return null;
        }

        /// <summary>
        /// ���������, ��� ���� �� �������� ������ �
        /// �������� ������ ����� ��� ���� �� ����� 10
        /// </summary>
        string GetDriverLicenseError(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "���� �� ����� ���� ������";
            if (Regex.IsMatch(text, "[\\D]+"))
                return "���� ����� ��������� ������ �����";
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}

