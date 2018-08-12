using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tesonet.Client.Converters
{
    public class IsCheckedToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isChecked = value as bool?;
            return isChecked.HasValue && isChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}