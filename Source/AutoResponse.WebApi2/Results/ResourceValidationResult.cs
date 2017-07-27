namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class ResourceValidationResult : AutoResponseResult
    {
        public ResourceValidationResult(HttpRequestMessage request, ValidationErrorDetails errorDetails)
            : base(request, new EntityValidationApiEvent(errorDetails))
        {
        }     
    }
}