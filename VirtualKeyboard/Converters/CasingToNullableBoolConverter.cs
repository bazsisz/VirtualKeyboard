using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using static VirtualKeyboard.ShiftManager;

namespace VirtualKeyboard.Converters
{
    [ValueConversion(typeof(Casing), typeof(bool?))]
    class CasingToNullableBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool?) &&
                value is Casing casing) 
            {
                switch (casing)
                {
                    case Casing.LowerCase:
                        return null;

                    case Casing.FirstLetterUpperCase:
                        return false;

                    default:
                        return true;
                }
            }
            throw new Exception("Error in CasingToNullableBoolConverter");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Casing))
            {
                bool? shiftState = (bool?)value;
                switch (shiftState)
                {
                    case null:
                        return Casing.LowerCase;

                    case false:
                        return Casing.FirstLetterUpperCase;

                    default:
                        return Casing.UpperCase;
                }
            }
            throw new Exception("Error in CasingToNullableBoolConverter");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
