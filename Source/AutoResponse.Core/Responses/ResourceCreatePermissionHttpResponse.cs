// <copyright file="ResourceCreatePermissionHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;
    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Resource create permission HTTP response.
    /// </summary>
    public class ResourceCreatePermissionHttpResponse : JsonHttpResponse<ResourcePermissionApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatePermissionHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceCreatePermissionHttpResponse(string message, string code, string userId, string resource, string resourceId)
          : base(new ResourcePermissionApiModel { Message = message, Code = code, UserId = userId, Resource = resource, ResourceId = resourceId }, HttpStatusCode.Forbidden)
        {
        }
    }
}