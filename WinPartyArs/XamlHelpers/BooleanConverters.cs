using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WinPartyArs.XamlHelpers
{
    public class BooleanToHiddenVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, true) ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Equals(value, Visibility.Visible));
    }

    public class BooleanToVisibilityInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, true) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(Equals(value, Visibility.Visible));
    }

    public class BooleanInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !Equals(value, true);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !Equals(value, true);
    }
}
