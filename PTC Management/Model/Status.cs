namespace PTC_Management.Model
{
    internal class Status
    {
        public const string noWorking = "Н";
        public const string vacation = "О";
        public const string working = "Р";
        public const string free = "С";

        public string NoWorking => noWorking;
        public string Vacation => vacation;
        public string Working => working;
        public string Free => free;

    }
}
