using PTC_Management.EntityFramework.Entityes.Base;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PTC_Management.EntityFramework
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
        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

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

        public override Entity Clone() => Clone<LogOfDepartureAndEntry>();

        /// <summary>
        /// Получение связанных сущностей из базы данных
        /// для избежания ошибки связанной с использованием 
        /// объектов разных контекстов
        /// </summary>
        public void SetEntities() => Itinerary = repository.GetSingle<Itinerary>(IdItinerary);

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
