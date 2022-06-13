using System.Collections.Generic;

namespace PTC_Management.Model
{
    internal class Visibility
    {
        public Dictionary<string, string> Field { get; set; }

        public const string collapsed = "Collapsed";
        public const string visible = "Visible";
    }
}
