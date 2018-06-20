// <copyright file="ResourceCreatePermissionResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource create permission result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class ResourceCreatePermissionResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatePermissionResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        public ResourceCreatePermissionResult(
            HttpRequestMessage request,
            string userId,
            string resourceType)
            : base(request, new EntityCreatedApiEvent(userId, resourceType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatePermissionResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceCreatePermissionResult(
            HttpRequestMessage request,
            string userId,
            string resourceType,
            string resourceId)
            : base(request, new EntityCreatedApiEvent(userId, resourceType, resourceId))
        {
        }
    }
}