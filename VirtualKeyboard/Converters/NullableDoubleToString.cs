using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboard.Converters
{
    [ValueConversion(typeof(double?), typeof(string))]
    class NullableDoubleToString : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                double? number = value as double?;
                return number == null ? "" : number.ToString();
            }
            throw new Exception("Error in StringToNullableDouble");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(double?) &&
                value is string str)
            {
                double.TryParse(str, out double result);
                return result;
            }
            throw new Exception("Error in StringToNullableDouble");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
