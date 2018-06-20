// <copyright file="ResourcePermissionResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource permission result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class ResourcePermissionResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePermissionResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourcePermissionResult(HttpRequestMessage request, string userId, string resourceType, string resourceId)
            : base(request, new EntityPermissionApiEvent(userId, resourceType, resourceId))
        {
        }
    }
}