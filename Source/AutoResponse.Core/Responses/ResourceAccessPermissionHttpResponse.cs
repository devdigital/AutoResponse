using AutoResponse.Core.Dtos;

namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;

    public class ResourceAccessPermissionHttpResponse : JsonHttpResponse<ResourcePermissionApiModel>
    {
        public ResourceAccessPermissionHttpResponse(string message, string code, string userId, string resource, string resourceId)
            : base(new ResourcePermissionApiModel { Message = message, Code = code, UserId = userId, Resource = resource, ResourceId = resourceId }, HttpStatusCode.Forbidden)
        {
        }
    }
}