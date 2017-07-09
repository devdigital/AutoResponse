namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    public class ResourceNotFoundHttpResponse : JsonHttpResponse<ValidationErrorDetailsDto>
    {      
        public ResourceNotFoundHttpResponse(string resourceType, string resourceId)
            : base(ToValidationErrorDetails(resourceType, resourceId), HttpStatusCode.NotFound)
        {            
        }

        private static ValidationErrorDetailsDto ToValidationErrorDetails(string resourceType, string resourceId)
        {
            return new ValidationErrorDetailsDto
            {
                Message = $"The {resourceType} resource with identifier '{resourceId}' was not found.",
                Errors = new List<ValidationErrorDto>
                {
                    new ValidationErrorDto
                    {
                        Resource = resourceType,
                        Field = "id",
                        Code = "missing",
                    }
                }
            };
        }
    }
}