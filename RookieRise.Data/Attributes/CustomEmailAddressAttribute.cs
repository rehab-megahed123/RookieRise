using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RookieRise.Data.Attributes
{
    public class CustomEmailAddressAttribute : ValidationAttribute
    {
        private static readonly Regex _regex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is string email)
            {
                if (_regex.IsMatch(email))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("The email address is not in a valid format.");
        }
    }
}
