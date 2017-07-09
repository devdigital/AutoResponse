namespace AutoResponse.Core.Responses
{
    using System;
    using System.Net;

    using AutoResponse.Core.Dtos;

    public class ResourceCreatePermissionHttpResponse : JsonHttpResponse<ErrorDto>
    {
        public ResourceCreatePermissionHttpResponse(string userId, string resourceType)
          : base(ToErrorDto(userId, resourceType, resourceId: null), HttpStatusCode.Forbidden)
        {            
        }

        public ResourceCreatePermissionHttpResponse(string userId, string resourceType, string resourceId)
            : base(ToErrorDto(userId, resourceType, resourceId), HttpStatusCode.Forbidden)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException(nameof(resourceId));
            }
        }

        private static ErrorDto ToErrorDto(string userId, string resourceType, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                return new ErrorDto
                {
                    Message =
                        $"The user with identifier '{userId}', does not have permission to create a {resourceType} resource"            
                };
            }

            return new ErrorDto
            {
                Message = 
                    $"The user with identifier '{userId}', does not have permission to create a {resourceType} resource with resource identifier '{resourceId}'"
            };
        }
    }
}