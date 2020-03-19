using System;
using System.Globalization;
using System.Windows.Data;

namespace ChronometerWPF.Converters
{
    public class TimeSpanToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan)value;
            return timeSpan.ToString(@"hh\:mm\:ss", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
