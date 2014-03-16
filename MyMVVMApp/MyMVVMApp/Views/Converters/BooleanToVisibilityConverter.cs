using System;
using System.Windows;
using System.Windows.Data;

namespace MyMVVMApp.Views.Converters
{
    // Convierte True o False en Visible o Collapsed respectivamente. Si el parámetro es Invert, hace lo contrario.
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((bool)value) ^ ((parameter != null) && ((string)parameter).Equals("Invert"))) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
