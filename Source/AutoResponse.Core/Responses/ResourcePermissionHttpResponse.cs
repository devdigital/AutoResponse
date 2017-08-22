using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Net;

    public class ResourcePermissionHttpResponse : JsonHttpResponse<ResourcePermissionDto>
    {
        public ResourcePermissionHttpResponse(string message, string code, string userId, string resource, string resourceId)
            : base(new ResourcePermissionDto { Message = message, Code = code, UserId = userId, Resource = resource, ResourceId = resourceId }, HttpStatusCode.Forbidden)
        {
        }
    }
}