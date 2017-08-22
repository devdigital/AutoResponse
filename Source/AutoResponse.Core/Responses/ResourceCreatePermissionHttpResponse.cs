using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ResourceCreatePermissionHttpResponse : JsonHttpResponse<ResourcePermissionDto>
    {
        public ResourceCreatePermissionHttpResponse(string message, string code, string userId, string resource, string resourceId)
          : base(new ResourcePermissionDto { Message = message, Code = code, UserId = userId, Resource = resource, ResourceId = resourceId }, HttpStatusCode.Forbidden)
        {            
        }
    }
}