
namespace EventTracker.Phone.Core.Converters
{
    using System.Windows.Data;
    using System.Globalization;
    using System;

    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;

            return string.Format(parameter.ToString(), System.Convert.ToDateTime(value), culture);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
