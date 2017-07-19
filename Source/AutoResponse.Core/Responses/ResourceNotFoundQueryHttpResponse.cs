namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    public class ResourceNotFoundQueryHttpResponse : JsonHttpResponse<ValidationErrorDetailsDto>
    {
        public ResourceNotFoundQueryHttpResponse(string resourceType, IEnumerable<QueryParameter> parameters)
            : base(ToValidationErrorDetails(resourceType, parameters), HttpStatusCode.NotFound)
        {
        }

        private static ValidationErrorDetailsDto ToValidationErrorDetails(
            string resourceType, 
            IEnumerable<QueryParameter> parameters)
        {
            return new ValidationErrorDetailsDto
            {
                Message = $"The {resourceType} resource was not found with the specified parameters",
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