using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    public class IdadeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime val = System.Convert.ToDateTime(value);
            return (int)(DateTime.Now - val).TotalDays / 365;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}