// <copyright file="ResourceCreatedHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Resource created HTTP response.
    /// </summary>
    public class ResourceCreatedHttpResponse : JsonHttpResponse<ResourceCreatedApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCreatedHttpResponse"/> class.
        /// TODO: location header.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public ResourceCreatedHttpResponse(string message, string code, string resourceId)
            : base(new ResourceCreatedApiModel { Message = message, Code = code, Id = resourceId }, HttpStatusCode.Created)
        {
        }
    }
}