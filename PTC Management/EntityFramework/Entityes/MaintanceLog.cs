using PTC_Management.EntityFramework.Entityes.Base;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EF
{
    [Table("MaintanceLog")]
    public partial class MaintanceLog : TransportInfo
    {
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? SpeedometerInfoOnDeparture { get; set; }

        public int? SpeedometerInfoWhenReturning { get; set; }

        public int? Mileage { get; set; }

        public virtual Itinerary Itinerary { get; set; }
    }


    public partial class MaintanceLog : TransportInfo
    {
        public static readonly Repository<MaintanceLog> repository =
             new Repository<MaintanceLog>(new PTC_ManagementContext());

        // переопределение методов базового класса
        public override void Add()
        {
            Itinerary = repository.GetSingle<Itinerary>(IdItinerary);
            repository.Add(this);
        }

        public override void Update()
        {
            Itinerary = repository.GetSingle<Itinerary>(IdItinerary);
            repository.Update(this);
        }

        public override bool Remove() => repository.Remove(this);

        public override void Copy(int count)
        {
            Itinerary = repository.GetSingle<Itinerary>(IdItinerary);
            repository.Copy(this, count);
        }

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
                Itinerary = item.Itinerary;
            }
        }

        public override Entity Clone()
        {
            var maintanceLog = new MaintanceLog { Id = Id };
            maintanceLog.SetFields(this);

            return maintanceLog;
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
                    case "Itinerary":
                        if (Itinerary == null)
                            error = "Поле не может быть пустым";
                        break;
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
