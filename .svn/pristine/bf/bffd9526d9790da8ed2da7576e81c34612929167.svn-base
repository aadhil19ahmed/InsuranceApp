using System;
using System.Globalization;
using Xamarin.Forms;

namespace ACIA.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var status = (string)value;
                if(status == "Yes")
                {
                    return true;
                }
                else
                {
                    return false;
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
