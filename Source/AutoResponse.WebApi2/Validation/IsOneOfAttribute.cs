// <copyright file="IsOneOfAttribute.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Is one of validation attribute.
    /// Validates that a value is one of the specified values.
    /// </summary>
    public class IsOneOfAttribute : ValidationAttribute
    {
        private readonly string[] values;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsOneOfAttribute"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public IsOneOfAttribute(params string[] values)
        {
            this.values = values ?? throw new ArgumentNullException(nameof(values));
        }

        /// <inheritdoc />
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