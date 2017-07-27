namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    public class ResourceNotFoundResult : AutoResponseResult
    {
        public ResourceNotFoundResult(HttpRequestMessage request, string resourceType, string resourceId)
            : base(request, new EntityNotFoundApiEvent(resourceType, resourceId))
        {            
        }        
    }
}