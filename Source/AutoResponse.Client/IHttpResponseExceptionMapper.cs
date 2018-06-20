// <copyright file="IHttpResponseExceptionMapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// HTTP response to exception mapper.
    /// </summary>
    public interface IHttpResponseExceptionMapper
    {
        /// <summary>
        /// Determines whether the response is an error response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>True if the response is an error; false otherwise.</returns>
        Task<bool> IsErrorResponse(HttpResponseMessage response);

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The exception.</returns>
        Task<Exception> GetException(HttpResponseMessage response);
    }
}