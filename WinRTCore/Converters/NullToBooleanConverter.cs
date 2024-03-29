﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace WinRTCore
{
    public sealed class NullToBooleanConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Inverted)
            {
                return value == null;
            }
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
