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
        /// ��������� ��������� ��������� �� ���� ������
        /// ��� ��������� ������ ��������� � �������������� 
        /// �������� ������ ����������
        /// </summary>
        public void SetEntities()
        {
            Employee = repository.GetSingle<Employee>(IdEmployee);
            Route = repository.GetSingle<Route>(IdRoute);
            Transport = repository.GetSingle<Transport>(IdTransport);
        }
        // ��������������� ������� �������� ������
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

                if (Employee == null)
                    Employee = itinerary.Employee;
                else
                    Employee.SetFields(itinerary.Employee);

                if (Route == null)
                    Route = itinerary.Route;
                else
                    Route.SetFields(itinerary.Route);

                if (Transport == null)
                    Transport = itinerary.Transport;
                else
                    Transport.SetFields(itinerary.Transport);


                TimeOnDeparture = itinerary.TimeOnDeparture;
                TimeWhenReturning = itinerary.TimeWhenReturning;
                Date = itinerary.Date;
                SpeedometerInfoOnDeparture = itinerary.SpeedometerInfoOnDeparture;
                SpeedometerInfoWhenReturning = itinerary.SpeedometerInfoWhenReturning;
                Mileage = itinerary.Mileage;
            }
        }

        public override Entity Clone() => Clone<Itinerary>();

        // ������������, ����� ���� �� �������������� ��� �������� ����
        bool[] canExecute = new bool[2];

        public bool GetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                if (canExecute[i] != true) return false;
            }

            if (Employee.Id == 0) return false;
            if (Route.Id == 0) return false;
            if (Transport.Id == 0) return false;

            return true;
        }

        public void SetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                canExecute[i] = true;
            }

            // ����� �������, �� ������� ���������� �������� ���������
            RaisePropertyChanged(nameof(SpeedometerInfoOnDeparture));
            RaisePropertyChanged(nameof(SpeedometerInfoWhenReturning));
            RaisePropertyChanged(nameof(Mileage));

            Employee.SetCanExecute();
            Route.SetCanExecute();
            Transport.SetCanExecute();
        }


        /// <summary>
        /// ���������� ���������� IDataErrorInfo
        /// ��������� ������������ ������,
        /// ����������� � ����� ��� �����
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
                            error = "���� �� ����� ���� ������";
                        break;
                    case "SpeedometerInfoOnDeparture":
                        if (SpeedometerInfoOnDeparture.HasValue)
                            canExecute[0] = true;

                        if (canExecute[0])
                            error =
                            IntError(SpeedometerInfoOnDeparture,
                                "��������� ���������� ������ ���� ������ ����");
                        break;
                    case "SpeedometerInfoWhenReturning":
                        if (SpeedometerInfoWhenReturning.HasValue)
                            canExecute[1] = true;

                        if (canExecute[1])
                            error =
                            IntError(SpeedometerInfoWhenReturning,
                                "��������� ���������� ������ ���� ������ ����");
                        break;
                    case "Mileage":
                        if (Mileage <= 0) error =
                                 "������ ������ ���� ������ ����";
                        break;

                }
                return error;
            }
        }

        public string IntError(int? number, string messageOnNegative)
        {
            if (!number.HasValue)
                return "���� �� ����� ���� ������";
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
