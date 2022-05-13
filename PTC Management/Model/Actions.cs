﻿namespace PTC_Management.Model.Dialog
{
    /// <summary>
    /// Список контстант для выбора действия, которое необходимо выполнить
    /// на основе параметра, передаваемого кнопкой
    /// </summary>
    
    // TODO: можно сделать статическим
    internal class Actions
    {
        internal const string add = "add";
        internal const string update = "update";
        internal const string remove = "remove";
        internal const string copy = "copy";

        internal const string writeAndClose = "writeAndClose";
        internal const string write = "write";
        internal const string close = "close";

        public string Add => add;
        public string Update => update;
        public string Remove => remove;
        public string Copy => copy;

        public static string WriteAndClose => writeAndClose;
        public string Write => write;
        public string Close => close;

        public string GetGenetiveName(string action)
        {
            switch (action)
            {
                case add: return "добавления";
                case update: return "изменения";
                case copy: return "копирования";
                default: return null;
            }
        }
    }
}
