using System;
using System.Globalization;
using System.Windows.Data;

namespace PTC_Management.Model
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            TimeSpan.TryParse(value.ToString(), out TimeSpan time);
            return time.ToString("h':'mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            TimeSpan.TryParse(value.ToString(), out TimeSpan time);

            var split = value.ToString().Split(':');

            string hours = null, minutes = null;

            if (split.Length > 1)
            {
                hours = split[0];
                minutes = split[1];

                if (int.TryParse(hours, out _))
                {
                    if (hours.Length > 2) hours = hours.Remove(2);
                }

                if (int.TryParse(minutes, out _))
                {
                    if (minutes.Length > 2) minutes = minutes.Remove(2);
                }

            }

            var result = string.Join(":", new string[] { hours, minutes });

            if (TimeSpan.TryParse(result, out _)) return result;

            return time.ToString("h':'mm");
        }

    }

}
