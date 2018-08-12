using System;
using System.Globalization;
using System.Windows.Data;
using TesonetWpfApp.Utils;
using Xceed.Wpf.Toolkit;

namespace TesonetWpfApp.Converters
{
    public class PasswordBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new WatermarkPasswordBoxWrapper(value as WatermarkPasswordBox);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
