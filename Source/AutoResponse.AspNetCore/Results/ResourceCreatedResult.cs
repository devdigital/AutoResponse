using AutoResponse.Core.ApiEvents;

namespace AutoResponse.AspNetCore.Results
{
    public class ResourceCreatedResult : AutoResponseResult
    {
        public ResourceCreatedResult(            
            string userId,
            string resourceType)       
            : base(new EntityCreatedApiEvent(userId, resourceType))
        {
        }

        public ResourceCreatedResult(string userId, string resourceType, string resourceId)
            : base(new EntityCreatedApiEvent(userId, resourceType, resourceId))
        {            
        }
    }
}