namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;

    public class ResourceValidationResult : HttpResponseResult
    {
        public ResourceValidationResult(HttpRequestMessage request, ValidationErrorDetails errorDetails)
            : base(request, new ResourceValidationHttpResponse(errorDetails))
        {
        }     
    }
}