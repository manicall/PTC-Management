using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PTC_Management.Model
{
    public class ToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out _))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Ожидалось целое число");
        }

    }


}
