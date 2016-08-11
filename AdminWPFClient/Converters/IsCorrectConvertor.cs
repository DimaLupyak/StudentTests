using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminWPFClient.Converters
{
    class IsCorrectConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string request = (string)parameter;
            bool? isCorrect = value as bool?;
            if (isCorrect == null) return null;
            if (request == "Color")
            {
                if (isCorrect.Value) return "Lime";
                else return "Red";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
