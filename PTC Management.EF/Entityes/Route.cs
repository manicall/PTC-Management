namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
using System.Xml.Linq;

    [Table("Route")]
    public partial class Route : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        public int Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public float? Distant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        public static readonly Repository<Route> repository =
            new Repository<Route>(new PTC_ManagementContext());

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
                        if (Number == null)
                            error = "???? ?? ????? ???? ??????";
                        break;
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "???? ?? ????? ???? ??????";
                        else if (Name.Contains(" "))
                            error = "???? ?? ?????? ????????? ???????";
                        break;
                    case "Distant":
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
