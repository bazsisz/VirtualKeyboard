using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboard.Converters
{
    public class ButtonToFontSize : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType == typeof(double)) &&
              values[0] is double width &&
              values[1] is double height)
            {
                double area = width * height;
                if (area == 0)
                {
                    return 20d;
                }
                var a = Interpolate(area, 2050, 21828, 20, 75);
                return a;
            }
            throw new Exception("Error in ButtonToFontSize");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ButtonToFontSize cannot convert back!");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        private double Interpolate(double currentArea, double minArea, double maxArea, double minFontSize, double maxFontSize)
        {
            double newAreaRange = currentArea - minArea;
            double originalAreaRange = maxArea - minArea;
            return minFontSize + (maxFontSize - minFontSize) * (newAreaRange / originalAreaRange);
        }

    }
}
