namespace AutoResponse.Core.Extensions
{
    using System;
    using System.Linq;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    using Humanizer;

    internal static class ValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetailsApiModel ToDto(this ValidationErrorDetails validationErrorDetails, string code)
        {
            if (validationErrorDetails == null)
            {
                throw new ArgumentNullException(nameof(validationErrorDetails));
            }

            return new ValidationErrorDetailsApiModel
            {
                Message = validationErrorDetails.Message,
                Code = code,
                Errors = validationErrorDetails.Errors.Select(e => new ValidationErrorDto
                {
                    Message = e.Message,
                    Resource = e.Resource,
                    Field = e.Field,
                    Code = e.Code.ToString().Kebaberize()
                })
            };
        }
    }
}