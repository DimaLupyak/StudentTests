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
    class TestValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            TestViewModel test = (value as BindingGroup).Items[0] as TestViewModel;
            if (test.Name == null || test.Name == "")
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
