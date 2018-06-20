// <copyright file="ResourceNotFoundResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Resource not found result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class ResourceNotFoundResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceNotFoundResult(HttpRequestMessage request, string resourceType, string resourceId)
            : base(request, new EntityNotFoundApiEvent(resourceType, resourceId))
        {
        }
    }
}