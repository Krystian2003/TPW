using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationView.Converters
{
    public class PositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double position)
            {
                double radius = 0;
                if (parameter is double radiusValue)
                {
                    radius = radiusValue;
                }
                return position - radius;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}