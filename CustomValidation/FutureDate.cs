using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.CustomValidation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || ((DateTime) value).Date <= DateTime.Now.Date)
            {
                return new ValidationResult("Date must be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
