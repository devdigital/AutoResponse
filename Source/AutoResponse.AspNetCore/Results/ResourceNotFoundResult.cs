using AutoResponse.Core.ApiEvents;

namespace AutoResponse.AspNetCore.Results
{
    public class ResourceNotFoundResult : AutoResponseResult
    {
        public ResourceNotFoundResult(string resourceType, string resourceId)
            : base(new EntityNotFoundApiEvent(resourceType, resourceId))
        {            
        }        
    }
}