using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyMVVMApp.Views.Converters
{
    // Utilizado para poder hacer binding a un ImageBrush (p.ej. para la propiedad Background de un control Panorama)
    // http://www.geekchamp.com/tips/wp7-imagebrush-binding-problem-workaround
    public class ImageBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(value as string, UriKind.RelativeOrAbsolute));
            imageBrush.Opacity = 0.4;
            imageBrush.Stretch = Stretch.UniformToFill;
            return imageBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
