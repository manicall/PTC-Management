using System;
using System.Globalization;
using System.Windows.Data;

namespace PTC_Management.Model
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            TimeSpan.TryParse(value.ToString(), out TimeSpan time);
            return time.ToString("hh':'mm");
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            TimeSpan.TryParse(value.ToString(), out TimeSpan time);

            var split = value.ToString().Split(':');

            string hours = null, minutes = null;

            // алгоритм удаляет последний символ в числе,
            // если его можно преобразовать к числу и оно слишком большое
            if (split.Length > 1)
            {
                hours = split[0];
                minutes = split[1];

                int res;
                while (int.TryParse(hours, out res) && res > 23)
                {
                    hours = hours.Remove(hours.Length - 1);
                }

                while (int.TryParse(minutes, out res) && res > 59)
                {
                    minutes = minutes.Remove(minutes.Length - 1);
                }
            }

            var result = string.Join(":", new string[] { hours, minutes });

            // если удалось преобразовать,
            // то возвращаем время полученное на основе алгоритма
            if (TimeSpan.TryParse(result, out _)) return result;

            // иначе возвращаем 0:00
            return time.ToString("h':'mm");
        }
    }
}
