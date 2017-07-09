namespace AutoResponse.Core.Extensions
{
    using System.Linq;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    using Humanizer;

    internal static class ValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetailsDto ToApiModel(this ValidationErrorDetails validationErrorDetails)
        {
            return new ValidationErrorDetailsDto
            {
                Message = validationErrorDetails.Message,
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