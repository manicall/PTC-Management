namespace PTC_Management.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

    [Table("LogOfDepartureAndEntry")]
    public partial class LogOfDepartureAndEntry : Entity
    {
        public int IdTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan TimeOnDeparture { get; set; }

        public TimeSpan TimeWhenReturning { get; set; }

        public virtual Transport Transport { get; set; }
    }

    public partial class LogOfDepartureAndEntry : Entity
    {
        public static readonly Repository<LogOfDepartureAndEntry> repository =
            new Repository<LogOfDepartureAndEntry>(new PTC_ManagementContext());

        // переопределение методов базового класса
        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is LogOfDepartureAndEntry item)
            {
                IdTransport = item.IdTransport;
                Date = item.Date;
                TimeOnDeparture = item.TimeOnDeparture;
                TimeWhenReturning = item.TimeWhenReturning;
            }
        }

        public override Entity Clone() { 
            return new LogOfDepartureAndEntry
            {
                Id = Id,
                IdTransport = IdTransport,
                Date = Date,
                TimeOnDeparture = TimeOnDeparture,
                TimeWhenReturning = TimeWhenReturning
            };
        }
    }

}
