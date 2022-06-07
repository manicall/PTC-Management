using PTC_Management.EntityFramework.Entityes.Base;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PTC_Management.EntityFramework
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

        /// <summary>
        /// ������������ �������� � ��������
        /// </summary>
        public void Attach(PTC_ManagementContext db)
        {
            db.Itinerary.Attach(Itinerary);
        }

        /// <summary>
        /// ����������� �������� �� ���������
        /// </summary>
        public void Detach(PTC_ManagementContext db)
        {
            db.Entry(Itinerary).State = EntityState.Detached;
        }

        // ��������������� ������� �������� ������
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
            }
        }

        public override Entity Clone() => Clone<MaintanceLog>();

        /// <summary>
        /// ��������� ��������� ��������� �� ���� ������
        /// ��� ��������� ������ ��������� � �������������� 
        /// �������� ������ ����������
        /// </summary>
        public void SetEntities() => Itinerary = repository.GetSingle<Itinerary>(IdItinerary);

        // ���������� ���������� IDataErrorInfo
        // ��������� ������������ ������,
        // ����������� � ����� ��� �����
        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Itinerary":
                        if (Itinerary == null)
                            error = "���� �� ����� ���� ������";
                        break;
                        //    break;
                        //case "Route":
                        //    if (Route == null)
                        //        error = "���� �� ����� ���� ������";
                        //    break;
                        //case "Transport":
                        //    if (Transport == null)
                        //        error = "���� �� ����� ���� ������";
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
