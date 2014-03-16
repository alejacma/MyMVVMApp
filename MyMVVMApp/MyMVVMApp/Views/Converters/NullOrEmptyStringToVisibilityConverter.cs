using System;
using System.Windows;
using System.Windows.Data;

namespace MyMVVMApp.Views.Converters
{
    // Convierte no Null y no vacío en Visible, y en Collapsed lo contrario. Si el parámetro es Invert, hace lo contrario.
    public class NullOrEmptyStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((!String.IsNullOrEmpty(value as string)) ^ ((parameter != null) && ((string)parameter).Equals("Invert"))) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
