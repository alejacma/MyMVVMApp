using System;
using System.Windows;
using System.Windows.Data;

namespace MyMVVMApp.Views.Converters
{
    // Convierte Null o no Null en False o True respectivamente. Si el parámetro es Invert, hace lo contrario.
    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((value != null) ^ ((parameter != null) && ((string)parameter).Equals("Invert")));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
