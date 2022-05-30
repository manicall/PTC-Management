namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Route")]
    public partial class Route : Entity, IDataErrorInfo
    {         
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        public int Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float Distant { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
    public partial class Route : Entity, IDataErrorInfo
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

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Number":
                        if (Number <= 0)
                            error = "Номер должен быть больше нуля";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Поле не может быть пустым";
                        break;
                    case "Distant":
                        if (Distant <= 0)
                            error = "Дистанция должна быть больше нуля";
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
