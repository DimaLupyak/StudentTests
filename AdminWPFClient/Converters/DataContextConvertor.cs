using AdminWPFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AdminWPFClient.Converters
{
    public class DataContextConvertor : IValueConverter
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
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
