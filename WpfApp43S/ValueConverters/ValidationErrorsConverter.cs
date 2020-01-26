using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp43S.ValueConverters
{
    [ValueConversion(typeof(IEnumerable<ValidationError>), typeof(string))]
    public class ValidationErrorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is IEnumerable<ValidationError> validationErrors) && validationErrors.Any())
            {
                return string.Join("\n", validationErrors.Select(e => e.ErrorContent));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
