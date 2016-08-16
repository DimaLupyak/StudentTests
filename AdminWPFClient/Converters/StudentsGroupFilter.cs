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
    public class StudentsGroupFilter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            IEnumerable<StudentViewModel> students = values[0] as IEnumerable<StudentViewModel>;
            int groupId = (int)values[1];
            if (students != null && groupId!=0)
            {
                return students.Where(x => x.GroupID == groupId);
            }
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
