namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceNotFoundHttpResponse : JsonHttpResponse<ValidationResponseDetailsDto>
    {      
        public ResourceNotFoundHttpResponse(string message, string code, string resourceType)
            : base(ToValidationErrorDetails(message, code, resourceType), HttpStatusCode.NotFound)
        {
        }

        private static ValidationResponseDetailsDto ToValidationErrorDetails(
            string message,
            string code,
            string resourceType)
        {
            return new ValidationResponseDetailsDto
            {
                Message = message,
                Code = code,
                Errors = new List<ValidationErrorDto>
                {
                    new ValidationErrorDto
                    {
                        Resource = resourceType,
                        Field = "id",
                        Code = "invalid"
                    }
                }
            };
        }
    }
}