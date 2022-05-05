using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    internal class Actions
    {
        internal const string _add = "create";
        internal const string _update = "update";
        internal const string _remove = "delete";
        internal const string _copy   = "copy";

        public string Add => _add;
        public string Update => _update;
        public string Remove => _remove;
        public string Copy   => _copy;
    }
}
