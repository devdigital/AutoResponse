namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    public class ResourceCreatePermissionResult : AutoResponseResult
    {        
        public ResourceCreatePermissionResult(
            HttpRequestMessage request,
            string userId,
            string resourceType)       
            : base(request, new EntityCreatedApiEvent(userId, resourceType))
        {            
        }

        public ResourceCreatePermissionResult(
            HttpRequestMessage request, 
            string userId, 
            string resourceType, 
            string resourceId)
            : base(request, new EntityCreatedApiEvent(userId, resourceType, resourceId))
        {
        }        
    }
}