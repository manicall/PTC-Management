namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Linq;

    [Table("Transport")]
    public partial class Transport : Entity, IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(9)]
        public string LicensePlate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogOfDepartureAndEntry> LogOfDepartureAndEntry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaintanceLog> MaintanceLog { get; set; }

        public static readonly Repository<Transport> repository =
            new Repository<Transport>(new PTC_ManagementContext());

        public override void Add()
        {
            repository.Add(this);
        }

        public override void Update()
        {
            repository.Update(this);
        }

        public override void Remove()
        {
            repository.Remove(this);
        }

        public override void Copy(int count)
        {
            repository.Copy(this, count);
        }

        public override void SetFields(Entity entity)
        {
            if (entity is Transport transport)
            {
                Name = transport.Name;
                LicensePlate = transport.LicensePlate;
            }
        }

        public override Entity Clone()
        {
            Transport transport = new Transport
            {
                Id = Id,
                Name = Name,
                LicensePlate = LicensePlate
            };

            return transport;
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Поле не может быть пустым";
                        else if (Name.Contains(" "))
                            error = "Поле не должно содержать пробелы";
                        break;
                    case "LicensePlate":
                        if (string.IsNullOrEmpty(LicensePlate))
                            error = "Поле не может быть пустым";
                        else if (LicensePlate.Contains(" "))
                            error = "Поле не должно содержать пробелы";
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
