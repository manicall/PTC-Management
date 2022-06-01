using PTC_Management.EF;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PTC_Management.EntityFramework.Entityes.Base
{
    public abstract class TransportInfo : Entity
    {
        public int IdItinerary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public DateTime? TimeOnDeparture { get; set; }

        public DateTime? TimeWhenReturning { get; set; }

    }
}
