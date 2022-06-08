using System;
using System.Globalization;
using System.Windows.Data;

namespace PTC_Management.Model
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            DateTime.TryParse(value.ToString(), out DateTime dt);
            return dt.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            DateTime.TryParse(value.ToString(), out DateTime dt);
            return dt.ToString("MM/dd/yyyy");
        }
    }
}
