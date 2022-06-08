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
            return time.ToString("hh':'mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            TimeSpan.TryParse(value.ToString(), out TimeSpan time);

            var split = value.ToString();

            if (split.Length > 1)
            {
                var hours = split.Split(':')[0];
                var minutes = split.ToString().Split(':')[1];

                if (int.TryParse(hours, out _))
                {
                    if (hours.Length > 2)
                    {
                        if (hours[0] == '0')
                            hours = hours.Remove(1, 1);
                        else
                            hours = hours.Remove(2);

                        return string.Join(":", new string[] { hours, minutes });
                    }
                }
            }
                

          


            return time.ToString("hh':'mm");

        }
    }
}
