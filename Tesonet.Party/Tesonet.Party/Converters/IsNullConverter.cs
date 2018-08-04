using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tesonet.Party.Converters
{
    public class IsNullConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isNull = value == null;
            return ((string)parameter) == "Invert" ? !isNull : isNull;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNullToVisibilityConverter : IsNullConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isNull = (bool)base.Convert(value, targetType, parameter, culture);
            return isNull ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class IsNullOrEmptyConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isNull = value == null;

            if (!isNull)
            {
                if (value is string)
                    isNull = string.IsNullOrWhiteSpace((string)value);
                else if (value is ICollection)
                    isNull = ((ICollection)value).Count == 0;
                else if (IsNumericType(value.GetType()))
                    isNull = System.Convert.ToDecimal(value) == 0;
                else if (value is DateTime)
                    isNull = ((DateTime)value) == DateTime.MinValue;
            }

            return ((string)parameter) == "Invert" ? !isNull : isNull;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static bool IsNumericType(Type type)
        {
            if (type == null)
                return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    return false;
            }
            return false;
        }

    }

    public class IsNullOrEmptyToVisibilityConverter : IsNullOrEmptyConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isNull = (bool)base.Convert(value, targetType, parameter, culture);
            return isNull ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
