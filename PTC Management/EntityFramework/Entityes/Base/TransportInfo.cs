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

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

    }
}
