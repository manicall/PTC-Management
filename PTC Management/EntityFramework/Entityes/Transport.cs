namespace PTC_Management.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Transport")]
    public partial class Transport : Entity, IDataErrorInfo
    {
         
        public Transport()
        {
            Itinerary = new HashSet<Itinerary>();
            LogOfDepartureAndEntry = new HashSet<LogOfDepartureAndEntry>();
            MaintanceLog = new HashSet<MaintanceLog>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string LicensePlate { get; set; }

        
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        
        public virtual ICollection<LogOfDepartureAndEntry> 
            LogOfDepartureAndEntry { get; set; }

        
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
                        break;
                    case "LicensePlate":
                        if (string.IsNullOrEmpty(LicensePlate))
                            error = "Поле не может быть пустым";
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
