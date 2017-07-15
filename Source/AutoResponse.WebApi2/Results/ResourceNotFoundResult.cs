namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Responses;

    public class ResourceNotFoundResult : HttpResponseResult
    {
        public ResourceNotFoundResult(HttpRequestMessage request, string resourceType, string resourceId)
            : base(request, new ResourceNotFoundHttpResponse(resourceType, resourceId))
        {            
        }        
    }
}