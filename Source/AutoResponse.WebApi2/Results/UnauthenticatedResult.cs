// <copyright file="UnauthenticatedResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Unauthenticated result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class UnauthenticatedResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="message">The message.</param>
        public UnauthenticatedResult(HttpRequestMessage request, string message)
            : base(request, new UnauthenticatedApiEvent(message))
        {
        }
    }
}