using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Transport")]
    public partial class Transport : Entity
    {
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string LicensePlate { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }
    }

    public partial class Transport : Entity
    {
        public static readonly Repository<Transport> repository =
            new Repository<Transport>();

        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Transport transport)
            {
                Name = transport.Name;
                LicensePlate = transport.LicensePlate;
            }
        }

        public override Entity Clone() => Clone<Transport>();

        // ������������, ����� ���� �� �������������� ��� �������� ����
        bool[] canExecute = new bool[2];

        public bool GetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                if (canExecute[i] != true) return false;
            }
            return true;
        }

        public void SetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                canExecute[i] = true;
            }

            // ����� �������, �� ������� ���������� �������� ���������
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(LicensePlate));
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (!string.IsNullOrEmpty(Name))
                            canExecute[0] = true;

                        if (canExecute[0])
                            if (string.IsNullOrEmpty(Name))
                            error = "���� �� ����� ���� ������";
                        break;
                    case "LicensePlate":
                        if (!string.IsNullOrEmpty(LicensePlate))
                            canExecute[1] = true;

                        if (canExecute[1])
                            if (string.IsNullOrEmpty(LicensePlate))
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
