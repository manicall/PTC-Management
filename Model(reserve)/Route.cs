namespace PTC_Management.Model
{
    internal class Route
    {
        public int idRoute { get; set; }
        public string name { get; set; }
        public float distant  { get; set; }

        public static Route[] GetInfo()
        {
            var info = new Route[] {
                new Route { idRoute = 25, name="Дом творчества (66 квартал) - Завод \"Амурсталь\" (ул.Заводская)", distant=18.8f }
            };
            return info;
        }
    }
}
