// <copyright file="ValidationErrorDetailsExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Extensions
{
    using System;
    using System.Linq;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    using Humanizer;

    /// <summary>
    /// Validation error details extensions.
    /// </summary>
    internal static class ValidationErrorDetailsExtensions
    {
        /// <summary>
        /// Converts validation error details to the dto.
        /// </summary>
        /// <param name="validationErrorDetails">The validation error details.</param>
        /// <param name="code">The code.</param>
        /// <returns>The DTO.</returns>
        public static ResourceValidationApiModel ToDto(this ValidationErrorDetails validationErrorDetails, string code)
        {
            if (validationErrorDetails == null)
            {
                throw new ArgumentNullException(nameof(validationErrorDetails));
            }

            return new ResourceValidationApiModel
            {
                Message = validationErrorDetails.Message,
                Code = code,
                Errors = validationErrorDetails.Errors.Select(e => new ResourceValidationErrorApiModel
                {
                    Message = e.Message,
                    Resource = e.Resource,
                    Field = e.Field,
                    Code = e.Code.ToString().Kebaberize(),
                }),
            };
        }
    }
}