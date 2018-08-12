using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tesonet.Client.Converters
{
    public class IsBusyToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBusy = value as bool?;
            return isBusy != null && isBusy.Value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}