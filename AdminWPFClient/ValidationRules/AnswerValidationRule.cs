using AdminWPFClient.ServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AdminWPFClient.ValidationRules
{
    class AnswerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            AnswerViewModel answer = (value as BindingGroup).Items[0] as AnswerViewModel;
            if (answer.Text == null && answer.Image == null)
            {
                return new ValidationResult(false,
                    "Введіть текст, або завантажте зображення");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
