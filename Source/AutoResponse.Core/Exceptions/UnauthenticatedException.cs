// <copyright file="UnauthenticatedException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Unauthenticated exception.
    /// </summary>
    public class UnauthenticatedException : AutoResponseException<UnauthenticatedApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public UnauthenticatedException(string code, string message)
            : base(message, new UnauthenticatedApiEvent(code, message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnauthenticatedException(string message)
            : base(message, new UnauthenticatedApiEvent(message))
        {
        }
    }
}
