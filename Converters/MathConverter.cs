using System;
using System.Globalization;
using System.Windows.Data;

namespace Digitamentos.Converters
{
    public class MathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d && parameter is string p)
            {
                if (p.StartsWith("@VALUE*"))
                {
                    if (double.TryParse(p.Substring(7), NumberStyles.Any, CultureInfo.InvariantCulture, out double multiplier))
                    {
                        return d * multiplier;
                    }
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
