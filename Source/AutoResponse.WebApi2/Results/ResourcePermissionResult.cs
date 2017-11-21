namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    public class ResourcePermissionResult : AutoResponseResult
    {
        public ResourcePermissionResult(HttpRequestMessage request, string userId, string resourceType, string resourceId)
            : base(request, new EntityPermissionApiEvent(userId, resourceType, resourceId))
        {
        } 
    }
}