namespace PTC_Management.EF
{
using PTC_Management.EntityFramework;

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MaintanceLog")]
    public partial class MaintanceLog : Entity
    {
        public int IdTransport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

        public int? SpeedometerInfoOnDeparture { get; set; }

        public int? SpeedometerInfoWhenReturning { get; set; }

        public int? Mileage { get; set; }

        public virtual Transport Transport { get; set; }

       
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
                IdTransport = item.IdTransport;
                Date = item.Date;
                TimeOnDeparture = item.TimeOnDeparture;
                TimeWhenReturning = item.TimeWhenReturning;
                SpeedometerInfoOnDeparture = item.SpeedometerInfoOnDeparture;
                SpeedometerInfoWhenReturning = item.SpeedometerInfoWhenReturning;
                Mileage = item.Mileage;
            }
        }

        public override Entity Clone()
        {
            return new MaintanceLog
            {
                IdTransport = IdTransport,
                Date = Date,
                TimeOnDeparture = TimeOnDeparture,
                TimeWhenReturning = TimeWhenReturning,
                SpeedometerInfoOnDeparture = SpeedometerInfoOnDeparture,
                SpeedometerInfoWhenReturning = SpeedometerInfoWhenReturning,
                Mileage = Mileage
            };
        }
    }
}
