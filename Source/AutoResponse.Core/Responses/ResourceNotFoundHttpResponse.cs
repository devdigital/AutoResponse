namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class ResourceNotFoundHttpResponse : ResourceValidationHttpResponse
    {        
        public ResourceNotFoundHttpResponse(string resourceType, string resourceId)
            : base(ToValidationErrorDetails(resourceType, resourceId))
        {            
        }

        private static ValidationErrorDetails ToValidationErrorDetails(string resourceType, string resourceId)
        {
            return new ValidationErrorDetails(
                $"The {resourceType} resource with identifier '{resourceId}' was not found.",
                new List<ValidationError>
                {
                    new ValidationError(resourceType, "id", ValidationErrorCode.Missing)
                });
        }
    }
}