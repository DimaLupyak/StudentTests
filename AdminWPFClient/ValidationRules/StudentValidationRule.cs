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
    class StudentValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            StudentViewModel student = (value as BindingGroup).Items[0] as StudentViewModel;
            if (student.Name == null || student.Name == "" || student.GroupID == 0)
            {
                return new ValidationResult(false,
                    "Не всі дані введені");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
