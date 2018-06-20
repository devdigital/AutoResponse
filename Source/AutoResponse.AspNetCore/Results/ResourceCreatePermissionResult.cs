// <copyright file="ResourceCreatePermissionResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource create permission result.
    /// </summary>
    /// <seealso cref="AutoResponse.AspNetCore.Results.AutoResponseResult" />
    public class ResourceCreatePermissionResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatePermissionResult"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        public ResourceCreatePermissionResult(
            string userId,
            string resourceType)
            : base(new EntityCreatedApiEvent(userId, resourceType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatePermissionResult"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceCreatePermissionResult(
            string userId,
            string resourceType,
            string resourceId)
            : base(new EntityCreatedApiEvent(userId, resourceType, resourceId))
        {
        }
    }
}