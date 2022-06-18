using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace PTC_Management.EntityFramework
{
    [Table("Route")]
    public partial class Route : Entity
    {
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        public int? Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal? Distant { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
    public partial class Route : Entity
    {
        public static readonly Repository<Route> repository =
            new Repository<Route>(new AppContext());

        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Route route)
            {
                Number = route.Number;
                Name = route.Name;
                Distant = route.Distant;
            }
        }

        public override bool CheckNulls()
        {
            bool result = true;

            var properties = GetType()
                .GetProperties()
                .Where(item => item.DeclaringType == typeof(Employee)
                && item.Name != "Item"
                && item.Name != "Error"
                && item.PropertyType.Name != "ICollection`1").ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetValue(this) == null)
                {
                    // ��������� ��� ������ � ������ �������
                    properties[i].SetValue(this, "");
                    result = false;
                    RaisePropertyChanged(properties[i].Name);
                }
            }

            return result;
        }

        public override Entity Clone() => Clone<Route>();
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Number":
                        error = IntError(Number);
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "���� �� ����� ���� ������";
                        break;
                    case "Distant":
                        error = DistantError(Distant);
                        break;

                }
                return error;
            }
        }

        public string IntError(int? number)
        {
            if (number == null)
                return "���� �� ����� ���� ������";
            if (number <= 0)
                return "����� ������ ���� ������ ����";
            return null;
        }

        public string DistantError(decimal? distant)
        {
            if (distant == null)
                return "���� �� ����� ���� ������";
            if (distant <= 0)
                return "��������� ������ ���� ������ ����";
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
