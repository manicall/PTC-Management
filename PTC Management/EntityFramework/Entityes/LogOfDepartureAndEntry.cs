using PTC_Management.EntityFramework.Entityes.Base;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EF
{
    [Table("LogOfDepartureAndEntry")]
    public partial class LogOfDepartureAndEntry : TransportInfo
    {

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public virtual Itinerary Itinerary { get; set; }
    }



    public partial class LogOfDepartureAndEntry : TransportInfo
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
                Id = item.Id;
                IdItinerary = item.IdItinerary;
                Date = item.Date;
                TimeOnDeparture = item.TimeOnDeparture;
                TimeWhenReturning = item.TimeWhenReturning;
                Itinerary = Itinerary;
            }
        }

        public override Entity Clone()
        {
            return new LogOfDepartureAndEntry
            {
                Id = Id,
                IdItinerary = IdItinerary,
                Date = Date,
                TimeOnDeparture = TimeOnDeparture,
                TimeWhenReturning = TimeWhenReturning,
                Itinerary = Itinerary
            };
        }
    }

}
