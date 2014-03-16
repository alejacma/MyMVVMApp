﻿using System;
using System.Windows;
using System.Windows.Data;

namespace MyMVVMApp.Views.Converters
{
    // Invierte un valor booleano
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
