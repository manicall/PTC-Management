using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Itinerary")]
    public partial class Itinerary : Entity
    {

        public Itinerary()
        {
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        public int IdRoute { get; set; }

        public int IdTransport { get; set; }

        public int IdEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Route Route { get; set; }

        public virtual Transport Transport { get; set; }

        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }

        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }
    }

    public partial class Itinerary : Entity
    {
        public static readonly Repository<Itinerary> repository =
            new Repository<Itinerary>(new PTC_ManagementContext());

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
                Employee = itinerary.Employee;
                Route = itinerary.Route;
                Transport = itinerary.Transport;
            }
        }

        public override Entity Clone() => Clone<Itinerary>();

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
