using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Xml.Linq;

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

        /// <summary>
        /// Присоединяет сущности в контекст
        /// </summary>
        public void Attach(PTC_ManagementContext db)
        {
            db.Employee.Attach(Employee);
            db.Route.Attach(Route);
            db.Transport.Attach(Transport);
        }

        /// <summary>
        /// Отсоединяет сущности от контекста
        /// </summary>
        public void Detach(PTC_ManagementContext db)
        {
            db.Entry(Employee).State = EntityState.Detached;
            db.Entry(Route).State = EntityState.Detached;
            db.Entry(Transport).State = EntityState.Detached;
        }

        // переопределение методов базового класса
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

        public override Entity Clone() => Clone<Itinerary>();

        public override Entity DeepClone() {
            Itinerary itinerary = (Itinerary)Clone();
            itinerary.Employee = (Employee)Employee.Clone();
            itinerary.Route = (Route)Route.Clone();
            itinerary.Transport = (Transport)Transport.Clone();

            return itinerary;
        }

        /// <summary>
        /// реализация интерфейса IDataErrorInfo
        /// позволяет обрабатывать ошибки,
        /// допускаемые в полях для ввода
        /// </summary>
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Id":
                        if (string.IsNullOrEmpty(Id.ToString()))
                            error = "пустое";
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
