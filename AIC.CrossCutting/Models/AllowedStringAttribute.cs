using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AIC.CrossCutting.Models
{
    public class AllowedStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(value)) && !Validations.IsSecureString(Convert.ToString(value)))
                return new ValidationResult(GetErrorMessage());
                //throw new Exception("This string is not allowed");
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This string is not allowed!";
        }
    }

    public class Validations
    {
        public static bool IsSecureString(string fileName)
        {
            var stringRegix = @"^[a-zA-Z0-9_]*$";
            Match match = Regex.Match(fileName, stringRegix, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            return match.Success;
        }
    }

}

