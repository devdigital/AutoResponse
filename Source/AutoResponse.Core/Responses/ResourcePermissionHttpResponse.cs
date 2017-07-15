namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourcePermissionHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ResourcePermissionHttpResponse(string userId, string resourceType, string resourceId)
            : base(ToErrorDto(userId, resourceType, resourceId), HttpStatusCode.Forbidden)
        {
        }

        private static ErrorDto ToErrorDto(string userId, string resourceType, string resourceId)
        {
            return new ErrorDto
            {
                Message =
                    $"The user with identifier '{userId}', does not have permission to access the {resourceType} resource with identifier '{resourceId}'"
            };
        }
    }
}