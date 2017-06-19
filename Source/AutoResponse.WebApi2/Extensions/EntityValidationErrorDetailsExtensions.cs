namespace AutoResponse.WebApi2.Extensions
{
    using System.Linq;

    using AutoResponse.Data.Errors;
    using AutoResponse.WebApi2.ExceptionHandling;
    using AutoResponse.WebApi2.Results;

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