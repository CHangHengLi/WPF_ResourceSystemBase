using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFThemingDemo.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool reverse = parameter as string == "Reverse";
            bool boolValue = value is bool val && val;

            if (reverse)
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool reverse = parameter as string == "Reverse";
            bool isVisible = value is Visibility vis && vis == Visibility.Visible;

            if (reverse)
            {
                return !isVisible;
            }
            else
            {
                return isVisible;
            }
        }
    }
} 