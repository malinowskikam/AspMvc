using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspMvc.Validators
{
    public class RatingAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double x = 0.0;
            try
            {
                x = (double)value;
            } catch
            {
                return new ValidationResult("Pole musi zawierać liczbę zmiennoprzecinkową");
            }

            if (x < 1.0 || x > 5.0)
            {
                return new ValidationResult("Wartość musi być pomiędzy 1 a 5");
            }

            return ValidationResult.Success;
        }
    }
}