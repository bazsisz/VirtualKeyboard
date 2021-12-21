using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboard.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityHiddenConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool isVisible) &&
                (targetType == typeof(Visibility)))
            {
                if (parameter != null)
                {
                    return isVisible ? Visibility.Hidden : Visibility.Visible;
                }
                return isVisible ? Visibility.Visible : Visibility.Hidden;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is Visibility visibility) &&
                (targetType == typeof(bool)))
            {
                if (parameter != null)
                {
                    return visibility != Visibility.Visible;
                }
                return visibility == Visibility.Visible;
            }
            else
            {
                return null;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

