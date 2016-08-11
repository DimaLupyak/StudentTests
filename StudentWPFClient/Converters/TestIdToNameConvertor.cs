using StudentWpfClient.ServiceReference;
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
    public class TestIdToNameConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StudentTestServiceClient service = new StudentTestServiceClient();
            int id = (int)value;
            return service.GetTests().Where(x => x.Id == id).FirstOrDefault().Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
