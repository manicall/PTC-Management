using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model.MainWindow
{
    internal class Actions
    {
        internal const string _create = "create";
        internal const string _update = "update";
        internal const string _delete = "delete";
        internal const string _copy   = "copy";

        public string Create => _create;
        public string Update => _update;
        public string Delete => _delete;
        public string Copy   => _copy;
    }
}
