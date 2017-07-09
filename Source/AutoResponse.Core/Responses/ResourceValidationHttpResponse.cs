namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Extensions;
    using AutoResponse.Core.Models;

    public class ResourceValidationHttpResponse : JsonHttpResponse<ValidationErrorDetailsDto>
    {
        public ResourceValidationHttpResponse(ValidationErrorDetails data)
            : base(data.ToApiModel(), (HttpStatusCode)422)
        {
        }
    }
}