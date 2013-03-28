using System;
using System.Windows;
using System.Windows.Data;

namespace WinRTCore
{
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (Inverted)
            {
                return value != null ? Visibility.Collapsed : Visibility.Visible;
            }
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
