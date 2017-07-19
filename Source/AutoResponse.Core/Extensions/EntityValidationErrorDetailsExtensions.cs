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
            IHttpResponseFormatter formatter)
        {
            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            return new ValidationErrorDetails(
                formatter.EntityMessageToResourceMessage(errorDetails.Message),                
                errorDetails.Errors.Select(e => new ValidationError(
                    formatter.EntityTypeToResource(e.EntityType),
                    formatter.EntityPropertyToField(e.EntityProperty),
                    e.Code.ToValidationErrorCode(),
                    formatter.EntityMessageToResourceMessage(e.Message))));
        }
    }
}