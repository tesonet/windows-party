using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace tesonet.windowsparty.wpfapp.Converters
{
    public class ErrorMessageVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
