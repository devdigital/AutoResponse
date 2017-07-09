namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoResponse.Core.Models;
    using AutoResponse.Core.Responses;

    using Humanizer;

    public class ResourceValidationResult : HttpResponseResult
    {
        public ResourceValidationResult(HttpRequestMessage request, ValidationErrorDetails errorDetails)
            : base(request, new ResourceValidationHttpResponse(errorDetails))
        {
        }     
    }
}