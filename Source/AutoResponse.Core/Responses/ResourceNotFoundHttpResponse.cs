// <copyright file="ResourceNotFoundHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Resource not found HTTP response.
    /// </summary>
    public class ResourceNotFoundHttpResponse : JsonHttpResponse<ResourceNotFoundApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceNotFoundHttpResponse(string message, string code, string resource, string resourceId)
            : base(new ResourceNotFoundApiModel { Message = message, Code = code, Resource = resource, ResourceId = resourceId }, HttpStatusCode.NotFound)
        {
        }
    }
}