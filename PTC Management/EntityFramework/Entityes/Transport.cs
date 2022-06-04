using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace PTC_Management.EF
{
    [Table("Transport")]
    public partial class Transport : Entity
    {

        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(10)]
        public string LicensePlate { get; set; }


        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }


    public partial class Transport : Entity
    {
        public static readonly Repository<Transport> repository =
            new Repository<Transport>(new PTC_ManagementContext());

        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Transport transport)
            {
                Name = transport.Name;
                LicensePlate = transport.LicensePlate;
            }
        }

        public override Entity Clone()
        {
            var transport = new Transport { Id = Id };
            transport.SetFields(this);

            return transport;
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Поле не может быть пустым";
                        break;
                    case "LicensePlate":
                        if (string.IsNullOrEmpty(LicensePlate))
                            error = "Поле не может быть пустым";
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
