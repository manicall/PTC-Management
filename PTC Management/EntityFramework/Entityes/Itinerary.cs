using System;
using System.Collections.Generic;
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



    public partial class Itinerary : Entity {
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
                IdRoute = IdRoute,
                IdTransport = IdTransport,
                IdEmployee = IdEmployee,
                Employee = Employee,
                Route = Route,
                Transport = Transport
            };
            return itinerary;
        }

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода
        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Employee":
                        if (Employee == null)
                            error = "Поле не может быть пустым";
                        break;
                    case "Route":
                         if (Route == null)
                            error = "Поле не может быть пустым";
                        break;
                    case "Transport":
                         if (Transport == null)
                            error = "Поле не может быть пустым";
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
