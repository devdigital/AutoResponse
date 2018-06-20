// <copyright file="ResourceNotFoundResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource not found result.
    /// </summary>
    /// <seealso cref="AutoResponse.AspNetCore.Results.AutoResponseResult" />
    public class ResourceNotFoundResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundResult"/> class.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceNotFoundResult(string resourceType, string resourceId)
            : base(new EntityNotFoundApiEvent(resourceType, resourceId))
        {
        }
    }
}