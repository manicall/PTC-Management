using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("MaintanceLog")]
    public partial class MaintanceLog : Entity
    {
        public int IdItinerary { get; set; }

        public virtual Itinerary Itinerary { get; set; }

        [StringLength(4)]
        public string MaintenanceType { get; set; }

    }

    public partial class MaintanceLog : Entity
    {
        public static readonly Repository<MaintanceLog> repository =
             new Repository<MaintanceLog>(new AppContext());

        // ��������������� ������� �������� ������
        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) =>
            repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is MaintanceLog item)
            {
                IdItinerary = item.IdItinerary;
                Itinerary = item.Itinerary;
                MaintenanceType = item.MaintenanceType;
            }
        }

        public override bool CheckNulls()
        {
            throw new NotImplementedException();
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
                    default:
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
