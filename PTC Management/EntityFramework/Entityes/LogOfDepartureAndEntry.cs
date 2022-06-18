using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("LogOfDepartureAndEntry")]
    public partial class LogOfDepartureAndEntry : Entity
    {
        public int IdItinerary { get; set; }

        public virtual Itinerary Itinerary { get; set; }
    }
    public partial class LogOfDepartureAndEntry : Entity
    {
        public static readonly Repository<LogOfDepartureAndEntry> repository =
            new Repository<LogOfDepartureAndEntry>(new AppContext());

        // ��������������� ������� �������� ������
        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is LogOfDepartureAndEntry item)
            {
                IdItinerary = item.IdItinerary;
                Itinerary = item.Itinerary;
            }
        }

        public override bool CheckNulls()
        {
            throw new NotImplementedException();
        }

        public override Entity Clone() => Clone<LogOfDepartureAndEntry>();

        /// <summary>
        /// ��������� ��������� ��������� �� ���� ������
        /// ��� ��������� ������ ��������� � �������������� 
        /// �������� ������ ����������
        /// </summary>
        public void SetEntities() => Itinerary = repository.GetSingle<Itinerary>(IdItinerary);

        // ���������� ���������� IDataErrorInfo
        // ��������� ������������ ������,
        // ����������� � ����� ��� �����
        public override string this[string columnName] { get => null; }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
