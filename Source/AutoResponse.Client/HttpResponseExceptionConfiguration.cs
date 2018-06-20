// <copyright file="HttpResponseExceptionConfiguration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// HTTP response exception configuration.
    /// </summary>
    public class HttpResponseExceptionConfiguration
    {
        private readonly IDictionary<ErrorRegistration,
            Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseExceptionConfiguration"/> class.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public HttpResponseExceptionConfiguration(
            IDictionary<ErrorRegistration, Func<ResponseContent, HttpResponseExceptionContext, Exception>> mappings)
        {
            this.mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
        }

        /// <summary>
        /// Adds a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="mapping">The mapping.</param>
        public void AddMapping(
            HttpStatusCode statusCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            this.AddMapping(statusCode, null, mapping);
        }

        /// <summary>
        /// Adds a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="mapping">The mapping.</param>
        public void AddMapping(
            HttpStatusCode statusCode,
            string errorCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            var errorRegistration = new ErrorRegistration(statusCode, errorCode);
            if (this.mappings.ContainsKey(errorRegistration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is already registered for response status code {errorRegistration.StatusCode} and error code {errorRegistration.ErrorCode}");
            }

            this.mappings.Add(errorRegistration, mapping);
        }

        /// <summary>
        /// Updates a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="mapping">The mapping.</param>
        public void UpdateMapping(
            HttpStatusCode statusCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            this.UpdateMapping(statusCode, null, mapping);
        }

        /// <summary>
        /// Updates a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="mapping">The mapping.</param>
        public void UpdateMapping(
            HttpStatusCode statusCode,
            string errorCode,
            Func<ResponseContent, HttpResponseExceptionContext, Exception> mapping)
        {
            var registration = new ErrorRegistration(statusCode, errorCode);

            if (!this.mappings.ContainsKey(registration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is not registered for response status code {statusCode} and error code {errorCode}");
            }

            this.mappings[registration] = mapping;
        }

        /// <summary>
        /// Removes a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public void RemoveMapping(HttpStatusCode statusCode)
        {
            this.RemoveMapping(statusCode, null);
        }

        /// <summary>
        /// Removes a mapping.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorCode">The error code.</param>
        public void RemoveMapping(HttpStatusCode statusCode, string errorCode)
        {
            var registration = new ErrorRegistration(statusCode, errorCode);

            if (!this.mappings.ContainsKey(registration))
            {
                throw new InvalidOperationException(
                    $"HTTP response to exception mapping is not registered for response status code {statusCode} and error code {errorCode}");
            }

            this.mappings.Remove(registration);
        }
    }
}