using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp43S.ValueConverters
{
    [ValueConversion(typeof(int?), typeof(int))]
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? (int)value + 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)value;

            if (index > 0)
            {
                return index - 1;
            }

            return null;
        }
    }
}
