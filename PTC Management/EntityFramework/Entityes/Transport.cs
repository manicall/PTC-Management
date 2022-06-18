using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PTC_Management.EntityFramework
{
    [Table("Transport")]
    public partial class Transport : Entity
    {
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string LicensePlate { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }

    public partial class Transport : Entity
    {
        public static readonly Repository<Transport> repository =
            new Repository<Transport>(new AppContext());

        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Transport transport)
            {
                Name = transport.Name;
                LicensePlate = transport.LicensePlate;
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
                    // провер€ть тип данных в других классах
                    properties[i].SetValue(this, "");
                    result = false;
                    RaisePropertyChanged(properties[i].Name);
                }
            }

            return result;
        }

        public override Entity Clone() => Clone<Transport>();
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "ѕоле не может быть пустым";
                        break;
                    case "LicensePlate":
                        if (string.IsNullOrEmpty(LicensePlate))
                            error = "ѕоле не может быть пустым";
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
