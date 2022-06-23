using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Itinerary")]
    public partial class Itinerary : Entity
    {
        private int? speedometerInfoOnDeparture;
        private int? speedometerInfoWhenReturning;
        private int? mileage;

        public Itinerary()
        {
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

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

        public int? Mileage
        {
            get => mileage;
            set => SetProperty(ref mileage, value);
        }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int IdRoute { get; set; }

        public int IdTransport { get; set; }

        public int IdEmployee { get; set; }

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Route Route { get; set; }

        public virtual Transport Transport { get; set; }

        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }

        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }
    }
    public partial class Itinerary : Entity
    {
        public static readonly Repository<Itinerary> repository =
            new Repository<Itinerary>();

        /// <summary>
        /// Получение связанных сущностей из базы данных
        /// для избежания ошибки связанной с использованием 
        /// объектов разных контекстов
        /// </summary>
        public void SetEntities()
        {
            Employee = repository.GetSingle<Employee>(IdEmployee);
            Route = repository.GetSingle<Route>(IdRoute);
            Transport = repository.GetSingle<Transport>(IdTransport);
        }

        // переопределение методов базового класса
        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

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
                TimeOnDeparture = itinerary.TimeOnDeparture;
                TimeWhenReturning = itinerary.TimeWhenReturning;
                Date = itinerary.Date;
                SpeedometerInfoOnDeparture = itinerary.SpeedometerInfoOnDeparture;
                SpeedometerInfoWhenReturning = itinerary.SpeedometerInfoWhenReturning;
                Mileage = itinerary.Mileage;
            }
        }

        public override Entity Clone() => Clone<Itinerary>();

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
                            error = "Поле не может быть пустым";
                        break;
                    case "SpeedometerInfoOnDeparture":
                        error =
                            IntError(SpeedometerInfoOnDeparture,
                                "Показания спидометра должны быть больше нуля");
                        break;
                    case "SpeedometerInfoWhenReturning":
                        error =
                            IntError(SpeedometerInfoWhenReturning,
                                "Показания спидометра должны быть больше нуля");
                        break;
                    case "Mileage":
                        error = IntError(Mileage, "Пробег должен быть больше нуля");
                        break;

                }
                return error;
            }
        }

        public string IntError(int? number, string messageOnNegative)
        {
            if (!number.HasValue)
                return "Поле не может быть пустым";
            if (number <= 0)
                return messageOnNegative;
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
