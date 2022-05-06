using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    /// <summary>
    /// Список контстант для выбора действия, которое необходимо выполнить
    /// на основе параметра, передаваемого кнопкой
    /// </summary>
    internal class Actions
    {
        internal const string _add = "create";
        internal const string _update = "update";
        internal const string _remove = "delete";
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
    }
}
