using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboard.Converters
{
    public class CharacterCasingConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType == typeof(object) || targetType == typeof(string)) &&
               values.Length == 2 &&
               (values[0] is char character) &&
               (values[1] is ShiftManager shiftManager))
            {
                return shiftManager.ApplyCasing(character, false).ToString();
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("CharacterCasingConverter cannot convert back!");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
