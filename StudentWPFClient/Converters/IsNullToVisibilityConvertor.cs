using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace StudentWpfClient.Converters
{
    public class IsNullToVisibilityConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string request = (string)parameter;
            if (request == "Null")
            {
                return value != null ? Visibility.Collapsed : Visibility.Visible; 
            }
            else if (request == "NoNull")
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (request == "True")
            {
                return value as bool? == true ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (request == "False")
            {
                return value as bool? == false ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
