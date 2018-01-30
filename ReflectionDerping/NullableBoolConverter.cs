using Avalonia.Markup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ReflectionDerping
{
    class NullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool x)
            {
                return x.ToString();
            }
            return "<NULL>";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && Boolean.TryParse(s.ToLowerInvariant(), out bool x))
            {
                return x;
            }
            return null;
        }
    }
}
