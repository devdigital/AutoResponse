namespace AutoResponse.Core.Models
{
    using AutoResponse.Core.Formatters;

    internal static class ValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetails ToFormatted(
            this ValidationErrorDetails errorDetails,
            IHttpResponseFormatter formatter)
        {
            // TODO: format
            return errorDetails;
        }
    }
}