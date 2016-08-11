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
    public class NewQuestionConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            QuestionControlViewModel newQuestion = (QuestionControlViewModel)value;

            string request = (string)parameter;
            if (request == "Visibility")
            {

                return newQuestion == null ? Visibility.Collapsed : Visibility.Visible; 
            }
            else if (request == "ButtonText")
            {
                return newQuestion == null ? "Добавити питання" : "Зберегти";
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
