// <copyright file="UnauthenticatedHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;
    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Unauthentication HTTP response.
    /// </summary>
    public class UnauthenticatedHttpResponse : JsonHttpResponse<ErrorApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public UnauthenticatedHttpResponse(string message, string code)
            : base(new ErrorApiModel { Message = message, Code = code }, HttpStatusCode.Unauthorized)
        {
        }
    }
}