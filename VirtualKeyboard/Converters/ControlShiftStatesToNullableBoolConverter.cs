using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using static VirtualKeyboard.WpfInputKeyboard;

namespace VirtualKeyboard.Converters
{
    [ValueConversion(typeof(ControlShiftStates), typeof(bool?))]
    public class ControlShiftStatesToNullableBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType == typeof(bool?) ||
                targetType == typeof(object)) &&
                value is ControlShiftStates casing) 
            {
                switch (casing)
                {
                    case ControlShiftStates.NotActive:
                        return null;

                    case ControlShiftStates.ActiveUntilButtonPressed:
                        return false;

                    default:
                        return true;
                }
            }
            throw new Exception("Error in ControlShiftStatesToNullableBoolConverter");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(ControlShiftStates))
            {
                bool? shiftState = (bool?)value;
                switch (shiftState)
                {
                    case null:
                        return ControlShiftStates.NotActive;

                    case false:
                        return ControlShiftStates.ActiveUntilButtonPressed;

                    default:
                        return ControlShiftStates.AlwaysActive;
                }
            }
            throw new Exception("Error in ControlShiftStatesToNullableBoolConverter");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
