// <copyright file="ResourcePermissionHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Resource permission HTTP response.
    /// </summary>
    public class ResourcePermissionHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePermissionHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public ResourcePermissionHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code = code }, HttpStatusCode.Forbidden)
        {
        }
    }
}