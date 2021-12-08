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
    public class CtrlPressedButtonEnableConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType == typeof(bool)) &&
               values.Length == 2 &&
               (values[0] is char character) &&
               (values[1] is bool isCtrlActive))
            {
                if (isCtrlActive)
                {
                    if (character == 'a' || 
                        character == 'c' ||
                        character == 'x' ||
                        character == 'v')
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("CtrlPressedButtonEnableConverter cannot convert back!");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
