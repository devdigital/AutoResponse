// <copyright file="IApiEventHttpResponseMapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Mappers
{
    using AutoResponse.Core.Responses;

    /// <summary>
    /// API event to HTTP response mapper.
    /// </summary>
    public interface IApiEventHttpResponseMapper
    {
        /// <summary>
        /// Gets the HTTP response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="apiEvent">The API event.</param>
        /// <returns>The HTTP response.</returns>
        IHttpResponse GetHttpResponse(
            object context,
            object apiEvent);
    }
}