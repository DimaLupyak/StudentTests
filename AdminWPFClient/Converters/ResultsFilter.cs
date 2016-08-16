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
    public class ResultsFilter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<ResultControlViewModel> results = values[0] as IEnumerable<ResultControlViewModel>;
            int? testId = values[1] as int?;
            int? groupId = values[2] as int?;
            int? studentId = values[3] as int?;
            if (results != null)
            {
                return results.Where(x => IsSelect(x, testId, groupId, studentId));
            }
            return null;
        }

        private bool IsSelect(ResultControlViewModel x, int? testId, int? groupId, int? studentId)
        {
            if (testId != null && x.Result.TestId != testId) 
                return false;
            if (groupId != null && studentId == null && x.Student.GroupID != groupId) 
                return false;
            if (studentId != null && x.Result.StudentId != studentId) 
                return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
