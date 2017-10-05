using AutoResponse.Core.ApiEvents;

namespace AutoResponse.AspNetCore.Results
{
    public class ResourcePermissionResult : AutoResponseResult
    {
        public ResourcePermissionResult(string userId, string resourceType, string resourceId)
            : base(new EntityPermissionApiEvent(userId, resourceType, resourceId))
        {
        } 
    }
}