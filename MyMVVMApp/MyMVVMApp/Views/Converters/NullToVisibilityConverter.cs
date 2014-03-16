using System;
using System.Windows;
using System.Windows.Data;

namespace MyMVVMApp.Views.Converters
{
    // Convierte no Null o Null en Visible o Collapsed respectivamente. Si el parámetro es Invert, hace lo contrario.
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((value != null) ^ ((parameter != null) && ((string)parameter).Equals("Invert"))) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
