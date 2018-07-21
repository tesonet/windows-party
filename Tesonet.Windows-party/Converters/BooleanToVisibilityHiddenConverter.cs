using System;
using System.Windows;
using System.Windows.Data;

namespace Tesonet.Windows_party.Converters
{
    class BooleanToVisibilityHiddenConverter : IValueConverter
    {     
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = value != null && (bool)value;
            if (bValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            if (visibility == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }
}
