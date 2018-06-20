// <copyright file="ServiceErrorException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Service error exception.
    /// </summary>
    public class ServiceErrorException : AutoResponseException<ServiceErrorApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ServiceErrorException(string code, string message)
            : base(message, new ServiceErrorApiEvent(code, message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="exception">The exception.</param>
        public ServiceErrorException(string code, Exception exception)
            : base(exception.Message, new ServiceErrorApiEvent(code, exception))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceErrorException(string message)
            : base(message, new ServiceErrorApiEvent(message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorException"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ServiceErrorException(Exception exception)
            : base(exception.Message, new ServiceErrorApiEvent(exception))
        {
        }
    }
}