// <copyright file="ValidationErrorDetailsExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Models
{
    using System.Linq;

    using AutoResponse.Core.Formatters;

    /// <summary>
    /// Validation error details extensions.
    /// </summary>
    internal static class ValidationErrorDetailsExtensions
    {
        /// <summary>
        /// Converts error details to formatted validation error detials.
        /// </summary>
        /// <param name="errorDetails">The error details.</param>
        /// <param name="formatter">The formatter.</param>
        /// <returns>The formatted validation error details.</returns>
        public static ValidationErrorDetails ToFormatted(
            this ValidationErrorDetails errorDetails,
            IAutoResponseExceptionFormatter formatter)
        {
            return new ValidationErrorDetails(
                formatter.Message(errorDetails.Message),
                errorDetails.Errors.Select(e => new ValidationError(
                    formatter.Resource(e.Resource),
                    formatter.Field(e.Field),
                    e.Code,
                    e.Message)));
        }
    }
}