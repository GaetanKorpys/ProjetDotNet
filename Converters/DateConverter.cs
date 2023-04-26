using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjetDotNet.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("dd/MM/yyyy");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value as string, out DateTime date))
            {
                return date;
            }

            return value;
        }
    }
}
