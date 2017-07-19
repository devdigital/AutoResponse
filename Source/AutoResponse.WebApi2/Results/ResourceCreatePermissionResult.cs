namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.Responses;

    public class ResourceCreatePermissionResult : HttpResponseResult
    {        
        public ResourceCreatePermissionResult(
            HttpRequestMessage request,
            string userId,
            string resourceType)       
            : base(request, new ResourceCreatePermissionHttpResponse(userId, resourceType))
        {
        }

        public ResourceCreatePermissionResult(
            HttpRequestMessage request, 
            string userId, 
            string resourceType, 
            string resourceId)
            : base(request, new ResourceCreatePermissionHttpResponse(userId, resourceType, resourceId))
        {
        }        
    }
}