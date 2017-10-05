using AutoResponse.Core.ApiEvents;

namespace AutoResponse.AspNetCore.Results
{
    public class ResourceCreatePermissionResult : AutoResponseResult
    {        
        public ResourceCreatePermissionResult(
            string userId,
            string resourceType)       
            : base(new EntityCreatedApiEvent(userId, resourceType))
        {            
        }

        public ResourceCreatePermissionResult(
            string userId, 
            string resourceType, 
            string resourceId)
            : base(new EntityCreatedApiEvent(userId, resourceType, resourceId))
        {
        }        
    }
}