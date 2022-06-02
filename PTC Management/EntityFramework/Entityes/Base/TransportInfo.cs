using PTC_Management.EF;

using System;

namespace PTC_Management.EntityFramework.Entityes.Base
{
    public abstract class TransportInfo : Entity
    {
        public int IdItinerary { get; set; }

        public TimeSpan? TimeOnDeparture { get; set; }

        public TimeSpan? TimeWhenReturning { get; set; }

    }
}
