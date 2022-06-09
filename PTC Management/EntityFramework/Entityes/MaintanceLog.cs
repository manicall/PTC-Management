using PTC_Management.EntityFramework.Entityes.Base;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PTC_Management.EntityFramework
{
    [Table("MaintanceLog")]
    public partial class MaintanceLog : TransportInfo
    {
        private int? speedometerInfoOnDeparture;
        private int? speedometerInfoWhenReturning;
        private int? mileage;

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }



        public int? SpeedometerInfoOnDeparture
        {
            get => speedometerInfoOnDeparture;
            set
            {
                speedometerInfoOnDeparture = value;
                if (speedometerInfoWhenReturning != null)
                    Mileage = speedometerInfoWhenReturning - speedometerInfoOnDeparture;
            }
        }

        public int? SpeedometerInfoWhenReturning
        {
            get => speedometerInfoWhenReturning;
            set
            {
                speedometerInfoWhenReturning = value;
                if (speedometerInfoOnDeparture != null)
                    Mileage = speedometerInfoWhenReturning - speedometerInfoOnDeparture;
                
            }
        }

        public int? Mileage { 
            get => mileage; 
            set => SetProperty(ref mileage, value); 
        }

        public virtual Itinerary Itinerary { get; set; }

        [StringLength(4)]
        public string MaintenanceType { get; set; }
    }

    public partial class MaintanceLog : TransportInfo
    {
        public static readonly Repository<MaintanceLog> repository =
             new Repository<MaintanceLog>(new PTC_ManagementContext());

        // переопределение методов базового класса
        public override void Add() => repository.Add(this);

        public override void Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override void Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

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
                MaintenanceType = item.MaintenanceType;
            }
        }

        public override Entity Clone() => Clone<MaintanceLog>();

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
