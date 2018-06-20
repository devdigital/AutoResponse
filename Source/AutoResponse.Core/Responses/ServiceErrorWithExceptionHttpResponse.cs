// <copyright file="ServiceErrorWithExceptionHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System;
    using System.Net;

    using AutoResponse.Core.Dtos;

    /// <summary>
    /// Service error with exception HTTP response.
    /// </summary>
    public class ServiceErrorWithExceptionHttpResponse : JsonHttpResponse<ErrorWithExceptionApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorWithExceptionHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="exceptionString">The exception string.</param>
        public ServiceErrorWithExceptionHttpResponse(string message, string code, string exceptionMessage, string exceptionString)
            : base(ToErrorWithException(message, code, exceptionMessage, exceptionString), HttpStatusCode.InternalServerError)
        {
        }

        private static ErrorWithExceptionApiModel ToErrorWithException(
            string message,
            string code,
            string exceptionMessage,
            string exceptionString)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(exceptionMessage))
            {
                throw new ArgumentNullException(nameof(exceptionMessage));
            }

            if (string.IsNullOrWhiteSpace(exceptionString))
            {
                throw new ArgumentNullException(nameof(exceptionString));
            }

            return new ErrorWithExceptionApiModel
            {
                Message = message,
                Code = code,
                ExceptionMessage = exceptionMessage,
                ExceptionString = exceptionString,
            };
        }
    }
}