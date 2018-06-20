// <copyright file="ResourcePermissionResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource permission result.
    /// </summary>
    /// <seealso cref="AutoResponse.AspNetCore.Results.AutoResponseResult" />
    public class ResourcePermissionResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePermissionResult"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourcePermissionResult(string userId, string resourceType, string resourceId)
            : base(new EntityPermissionApiEvent(userId, resourceType, resourceId))
        {
        }
    }
}