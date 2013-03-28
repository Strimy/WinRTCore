using System;
using System.Windows;
using System.Windows.Data;

namespace WinRTCore
{
    public sealed class NullToBooleanConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Inverted)
            {
                return value == null;
            }
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
