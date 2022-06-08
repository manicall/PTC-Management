using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
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

        //[StringLength(10)]
        public decimal? Distant { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }



    public partial class Route : Entity
    {
        public static readonly Repository<Route> repository =
            new Repository<Route>(new PTC_ManagementContext());

        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Route route)
            {
                Number = route.Number;
                Name = route.Name;
                Distant = route.Distant;
            }
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
                        error = NumberError(Number);
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Поле не может быть пустым";
                        break;
                    case "Distant":
                        error = DistantError(Distant);
                        break;

                }
                return error;
            }
        }


        public string NumberError(int? number)
        {
            if (!number.HasValue)
                return "Поле не может быть пустым";
            if (number <= 0)
                return "Номер должен быть больше нуля";
            return null;
        }

        public string DistantError(decimal? distant)
        {
            if (!distant.HasValue)
                return "Поле не может быть пустым";
            //float.TryParse(distant, out float i);
            if (distant <= 0)
                return "Дистанция должна быть больше нуля";
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
