using System.Globalization;
using System.Windows.Controls;

namespace PTC_Management.Model
{
    public class ToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (float.TryParse(value.ToString(), out _))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Ожидалась десятичная дробь");
        }
    }
}
