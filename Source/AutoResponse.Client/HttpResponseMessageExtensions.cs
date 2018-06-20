// <copyright file="HttpResponseMessageExtensions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// HTTP response message extensions.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Handles the errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="throwOnUnhandledResponses">if set to <c>true</c> throw on unhandled responses.</param>
        /// <returns>The task.</returns>
        public static async Task HandleErrors(this HttpResponseMessage response, bool throwOnUnhandledResponses = true)
        {
            await HandleErrors(response, new AutoResponseHttpResponseExceptionMapper(), throwOnUnhandledResponses);
        }

        /// <summary>
        /// Handles the errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="throwOnUnhandledResponses">if set to <c>true</c> throw on unhandled responses.</param>
        /// <returns>The task.</returns>
        public static async Task HandleErrors(this HttpResponseMessage response, IHttpResponseExceptionMapper mapper, bool throwOnUnhandledResponses = true)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            var isErrorResponse = await mapper.IsErrorResponse(response);
            if (isErrorResponse)
            {
                var exception = await mapper.GetException(response);
                if (exception != null)
                {
                    throw exception;
                }

                if (throwOnUnhandledResponses)
                {
                    // TODO: better exception information
                    throw new Exception(
                        $"There was an HTTP error with status code {response.StatusCode}");
                }
            }
        }
    }
}
