using PTC_Management.ViewModel;

using System.Collections.Generic;

namespace PTC_Management.Model
{
    public class IsEnabled : BindableBase
    {
        private Dictionary<string, string> field;

        public Dictionary<string, string> Field
        {
            get => field;
            set => SetProperty(ref field, value);
        }

        /// <summary>
        /// Вызов события PropertyChanged
        /// </summary>
        public new void RaisePropertyChanged(string propertyName = "Field")
        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}
