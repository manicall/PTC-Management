namespace PTC_Management.Model
{
    internal class Transport
    {
        public int idTransport { get; set; }
        public string name { get; set; }
        public string licensePlate { get; set; }

        public static Transport[] GetInfo()
        {
            var info = new Transport[] {
                new Transport { idTransport = 1 },
                new Transport { idTransport = 2 },
                new Transport { idTransport = 3 },
                new Transport { idTransport = 4 },
                new Transport { idTransport = 5 },
                new Transport { idTransport = 6 }
            };
            return info;
        }
    }
}
