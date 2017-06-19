namespace AutoResponse.WebApi2.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class IsOneOfAttribute : ValidationAttribute
    {
        private readonly string[] values;

        public IsOneOfAttribute(params string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            this.values = values;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = value as string;
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return ValidationResult.Success;
            }

            if (this.values.Contains(stringValue))
            {
                return ValidationResult.Success;
            }
            
            var validValues = string.Join(", ", this.values.Select(v => $"'{v}'"));
            return new ValidationResult(
                $"The {validationContext.MemberName} value of '{stringValue}' is invalid. It must be one of the following values: {validValues}");
        }
    }
}