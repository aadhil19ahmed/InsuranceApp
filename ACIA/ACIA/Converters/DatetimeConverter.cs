using System;
using System.Globalization;
using Xamarin.Forms;

namespace ACIA.Converters
{
    public class DatetimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return DateTime.Now;
            }

            if (value is string)
            {
                var date = (string)value;
                return DateTime.ParseExact(date, "MM/dd/yyyy", null);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;

                return date.ToString("MM/dd/yyyy");
            }
            return value;
        }
    }
}
