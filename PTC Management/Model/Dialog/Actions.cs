using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model.Dialog
{
    /// <summary>
    /// Список контстант для выбора действия, которое необходимо выполнить
    /// на основе параметра, передаваемого кнопкой
    /// </summary>
    internal class Actions
    {
        internal const string _add = "add";
        internal const string _update = "update";
        internal const string _remove = "remove";
        internal const string _copy   = "copy";

        internal const string _writeAndClose = "writeAndClose";
        internal const string _write = "write";
        internal const string _close = "close";

        public string Add => _add;
        public string Update => _update;
        public string Remove => _remove;
        public string Copy   => _copy;    
        
        public string WriteAndClose => _writeAndClose;
        public string Write => _write;
        public string Close => _close;

        public string GetGenetiveName(string action) {
            switch (action) {
                case _add:    return "добавления";
                case _update: return "изменения";
                case _copy:   return "копирования";
                default:      return null;
            }
        }
    }
}
