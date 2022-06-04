using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EF
{
    [Table("Itinerary")]
    public partial class Itinerary : Entity
    {

        public Itinerary()
        {
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        public int IdRoute { get; set; }

        public int IdTransport { get; set; }

        public int IdEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Route Route { get; set; }

        public virtual Transport Transport { get; set; }


        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }


        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }
    }



    public partial class Itinerary : Entity
    {
        public static readonly Repository<Itinerary> repository =
            new Repository<Itinerary>(new PTC_ManagementContext());

        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Itinerary itinerary)
            {
                Id = itinerary.Id;
                IdRoute = itinerary.IdRoute;
                IdTransport = itinerary.IdTransport;
                IdEmployee = itinerary.IdEmployee;
                Employee = itinerary.Employee;
                Route = itinerary.Route;
                Transport = itinerary.Transport;
            }
        }

        public override Entity Clone()
        {
            Itinerary itinerary = new Itinerary
            {
                Id = Id,
                IdRoute = IdRoute,
                IdTransport = IdTransport,
                IdEmployee = IdEmployee,
                // при создании новой записи необходимо
                // создавать копии сущностей, так как при добавлении записи
                // оригинальные сущности могут использовать
                // другой контекст, что приведет к исключению InvalidOperationException
                Employee = (Employee)Employee?.Clone(),
                Route = (Route)Route?.Clone(),
                Transport = (Transport)Transport?.Clone()
            };
            return itinerary;
        }

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода
        public override string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }


        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
