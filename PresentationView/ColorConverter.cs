using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PresentationView.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorName)
            {
                return new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(colorName));
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}