namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    public class ResourceNotFoundQueryHttpResponse : JsonHttpResponse<ValidationErrorDetailsDto>
    {
        public ResourceNotFoundQueryHttpResponse(string message, string code, string resourceType, IEnumerable<QueryParameter> parameters)
            : base(ToValidationErrorDetails(message, code, resourceType, parameters), HttpStatusCode.NotFound)
        {
        }

        private static ValidationErrorDetailsDto ToValidationErrorDetails(
            string message,
            string code,
            string resourceType, 
            IEnumerable<QueryParameter> parameters)
        {
            return new ValidationErrorDetailsDto
            {
                Message = message,
                Code = code,
                Errors = parameters.Select(p => new ValidationErrorDto
                {
                    Resource = resourceType,
                    Field = p.Key,
                    Code = "invalid",
                    Message = string.IsNullOrWhiteSpace(p.Value) 
                        ? null 
                        : $"The {resourceType} resource was not found with parameter value of {p.Value}"
                })
            };
        }
    }
}