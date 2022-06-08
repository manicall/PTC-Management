using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

using static System.Net.Mime.MediaTypeNames;

namespace PTC_Management.Model
{
    public class ToDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Regex.IsMatch(value.ToString(), @"^\d*\.?\d+$"))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Ожидалась десятичная дробь");
        }
    }
}
