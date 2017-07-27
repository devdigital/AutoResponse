namespace AutoResponse.Core.Models
{
    using System.Linq;

    using AutoResponse.Core.Formatters;

    internal static class ValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetails ToFormatted(
            this ValidationErrorDetails errorDetails,
            IHttpResponseFormatter formatter)
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