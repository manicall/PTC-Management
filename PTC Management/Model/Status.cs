using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    internal class Status
    {
        public const string noWorking = "noWorking";
        public const string vacation = "vacation";
        public const string working = "working";
        public const string free = "free";

        public string NoWork => noWorking;
        public string Vacation => vacation;
        public string Working => working;
        public string Free => free;

    }
}
