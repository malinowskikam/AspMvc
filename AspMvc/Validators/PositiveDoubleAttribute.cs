using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspMvc.Validators
{
    public class PositiveDoubleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double x=0.0;
            try
            {
                x = (double)value;
            }
            catch
            {
                return new ValidationResult("Pole musi zawierać liczbę zmiennoprzecinkową");
            }

            if (x<=0.0)
            {
                return new ValidationResult("Wartość musi być dodatnia");
            }

            return ValidationResult.Success;
        }
    }
}