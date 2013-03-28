using System;
using System.Windows;
using System.Windows.Data;


namespace WinRTCore
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Inverted)
            {
                return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
            }
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
