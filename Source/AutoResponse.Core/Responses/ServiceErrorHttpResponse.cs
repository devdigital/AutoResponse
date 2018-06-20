// <copyright file="ServiceErrorHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;
    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Service error HTTP response.
    /// </summary>
    public class ServiceErrorHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public ServiceErrorHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code = code }, HttpStatusCode.InternalServerError)
        {
        }
    }
}