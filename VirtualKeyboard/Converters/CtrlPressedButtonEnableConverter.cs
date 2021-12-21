using System;
using System.Globalization;
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
               values.Length == 2)
            {
                bool? isCtrlActive = values[1] as bool?;
                string str = values[0].ToString();
                if (isCtrlActive != null)
                {
                    if (str == "a" ||
                        str == "c" ||
                        str == "x" ||
                        str == "v")
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
            throw new Exception("Error in CtrlPressedButtonEnableConverter");
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
