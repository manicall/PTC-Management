using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EF
{
    [Table("Route")]
    public partial class Route : Entity
    {
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        public int? Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float? Distant { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }



    public partial class Route : Entity
    {
        public static readonly Repository<Route> repository =
            new Repository<Route>(new PTC_ManagementContext());

        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Route route)
            {
                Number = route.Number;
                Name = route.Name;
                Distant = route.Distant;
            }
        }

        public override Entity Clone()
        {
            Route route = new Route
            {
                Number = Number,
                Name = Name,
                Distant = Distant
            };
            return route;
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Number":
                        if (!Number.HasValue)
                            error = "���� �� ����� ���� ������";
                        if (Number <= 0)
                            error = "����� ������ ���� ������ ����";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "���� �� ����� ���� ������";
                        break;
                    case "Distant":
                        if (!Distant.HasValue)
                            error = "���� �� ����� ���� ������";
                        if (Distant <= 0)
                            error = "��������� ������ ���� ������ ����";
                        break;

                }
                return error;
            }
        }
        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
