namespace AutoResponse.Core.Extensions
{
    using System;
    using System.Linq;

    using AutoResponse.Core.Errors;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Core.Models;

    internal static class EntityValidationErrorDetailsExtensions
    {
        public static ValidationErrorDetails ToValidationErrorDetails(
            this EntityValidationErrorDetails errorDetails,
            IHttpResponseFormatter httpResponseFormatter)
        {
            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            if (httpResponseFormatter == null)
            {
                throw new ArgumentNullException(nameof(httpResponseFormatter));
            }

            return new ValidationErrorDetails(
                httpResponseFormatter.EntityMessageToResourceMessage(errorDetails.Message),                
                errorDetails.Errors.Select(e => new ValidationError(
                    httpResponseFormatter.EntityTypeToResource(e.EntityType),
                    httpResponseFormatter.EntityPropertyToField(e.EntityProperty),
                    e.Code.ToValidationErrorCode(),
                    httpResponseFormatter.EntityMessageToResourceMessage(e.Message))));
        }
    }
}