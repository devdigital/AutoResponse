namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Responses;

    public class ResourcePermissionResult : HttpResponseResult
    {
        public ResourcePermissionResult(HttpRequestMessage request, string userId, string resourceType, string resourceId)
            : base(request, new ResourcePermissionHttpResponse(userId, resourceType, resourceId))
        {
        } 
    }
}