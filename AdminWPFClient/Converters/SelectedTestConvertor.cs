using AdminWPFClient.ServiceReference;
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
    public class SelectedTestConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TestViewModel selectedTest = (TestViewModel)value;

            string request = (string)parameter;
            if (request == "Visibility")
            {

                return selectedTest == null ? Visibility.Collapsed : Visibility.Visible; 
            }
            
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
