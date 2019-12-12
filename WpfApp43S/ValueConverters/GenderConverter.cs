using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp43S.ValueConverters
{
    [ValueConversion(typeof(int?), typeof(int))]
    public class GenderConverter : MarkupExtension, IValueConverter
    {
        private static GenderConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new GenderConverter());
        }

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
