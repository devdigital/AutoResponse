namespace AutoResponse.Core.Extensions
{
    using System.Linq;

    using AutoResponse.Core.Errors;
    using AutoResponse.Core.Models;

    using Humanizer;

    internal static class EntityValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetails ToValidationErrorDetails(
            this EntityValidationErrorDetails errorDetails)
        {
            return new ValidationErrorDetails(
                errorDetails.Message,
                errorDetails.Errors.Select(e => new ValidationError(
                    e.EntityType.Kebaberize(),
                    e.EntityProperty.Kebaberize(),
                    e.Code.ToValidationErrorCode(),
                    e.Message)));
        }
    }
}