using System.Net;

namespace AutoResponse.Core.Extensions
{
    using System;
    using System.Linq;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    using Humanizer;

    internal static class ValidationErrorDetailsExtensions
    {
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
                    Code = e.Code.ToString().Kebaberize()
                })
            };
        }
    }
}