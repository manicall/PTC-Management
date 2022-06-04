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
                IdItinerary = item.IdItinerary;
                Date = item.Date;
                TimeOnDeparture = item.TimeOnDeparture;
                TimeWhenReturning = item.TimeWhenReturning;
                Itinerary = item.Itinerary;
            }
        }

        public override Entity Clone()
        {
            var logOfDepartureAndEntry = new LogOfDepartureAndEntry { Id = Id };
            logOfDepartureAndEntry.SetFields(this);

            return logOfDepartureAndEntry;
        }

        // реализация интерфейса IDataErrorInfo
        // позволяет обрабатывать ошибки,
        // допускаемые в полях для ввода
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    //case "Employee":
                    //    if (Employee == null)
                    //        error = "Поле не может быть пустым";
                    //    break;
                    //case "Route":
                    //    if (Route == null)
                    //        error = "Поле не может быть пустым";
                    //    break;
                    //case "Transport":
                    //    if (Transport == null)
                    //        error = "Поле не может быть пустым";
                    //    break;
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
