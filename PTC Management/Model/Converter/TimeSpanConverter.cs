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

                int res; 
                while (int.TryParse(hours, out res))
                {
                    if (res > 23) hours = hours.Remove(hours.Length - 1);
                    else break;
                }

                while (int.TryParse(minutes, out res))
                {
                    if (res > 59) minutes = minutes.Remove(minutes.Length - 1);
                    else break;
                }

            }

            var result = string.Join(":", new string[] { hours, minutes });

            if (TimeSpan.TryParse(result, out _)) return result;

            return time.ToString("h':'mm");
        }

    }

}
