// <copyright file="ErrorRegistration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client
{
    using System.Net;

    /// <summary>
    /// Error registration.
    /// </summary>
    public class ErrorRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRegistration"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorCode">The error code.</param>
        public ErrorRegistration(HttpStatusCode statusCode, string errorCode)
        {
            this.StatusCode = statusCode;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorRegistration"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public ErrorRegistration(HttpStatusCode statusCode)
            : this(statusCode, null)
        {
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; }
    }
}