
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.Validators
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);
            var result = false;
            if (str.Any(x => char.IsDigit(x))
                && str.Any(x => char.IsUpper(x))
                && str.Any(x => char.IsLower(x))
                && str.Length >= 8
                && str.Length <= 20
                && !str.Contains(" ")
                )
            {
                result = true;
            }

            return result;
        }
    }
}
