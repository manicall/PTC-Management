using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EF
{
    [Table("MaintanceLog")]
    public partial class MaintanceLog : Entity
    {
        public int IdItinerary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

        public int? SpeedometerInfoOnDeparture { get; set; }

        public int? SpeedometerInfoWhenReturning { get; set; }

        public int? Mileage { get; set; }

        public virtual Itinerary Itinerary { get; set; }
    }


    public partial class MaintanceLog : Entity
    {
        public static readonly Repository<MaintanceLog> repository =
             new Repository<MaintanceLog>(new PTC_ManagementContext());

        // переопределение методов базового класса
        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count) => repository.Copy(this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is MaintanceLog item)
            {
                IdItinerary = item.IdItinerary;
                Date = item.Date;
                TimeOnDeparture = item.TimeOnDeparture;
                TimeWhenReturning = item.TimeWhenReturning;
                SpeedometerInfoOnDeparture = item.SpeedometerInfoOnDeparture;
                SpeedometerInfoWhenReturning = item.SpeedometerInfoWhenReturning;
                Mileage = item.Mileage;
                Itinerary = Itinerary;
            }
        }

        public override Entity Clone()
        {
            return new MaintanceLog
            {
                IdItinerary = IdItinerary,
                Date = Date,
                TimeOnDeparture = TimeOnDeparture,
                TimeWhenReturning = TimeWhenReturning,
                SpeedometerInfoOnDeparture = SpeedometerInfoOnDeparture,
                SpeedometerInfoWhenReturning = SpeedometerInfoWhenReturning,
                Mileage = Mileage,
                Itinerary = Itinerary
            };
        }
    }
}
