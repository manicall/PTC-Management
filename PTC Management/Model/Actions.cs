namespace PTC_Management.Model
{
    /// <summary>
    /// Список контстант для выбора действия, 
    /// которое необходимо выполнить
    /// </summary>

    internal class Actions
    {
        public const string add = "add";
        public const string update = "update";
        public const string remove = "remove";
        public const string copy = "copy";

        public const string writeAndClose = "writeAndClose";
        public const string write = "write";
        public const string close = "close";

        public string Add => add;
        public string Update => update;
        public string Remove => remove;
        public string Copy => copy;

        public static string WriteAndClose => writeAndClose;
        public string Write => write;
        public string Close => close;

        public static string GetGenetiveName(string action)
        {
            switch (action)
            {
                case add: return "Создание";
                case update: return "Изменение";
                case copy: return "Копирование";
                default: return null;
            }
        }
    }
}
