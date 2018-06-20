// <copyright file="DomainValidationException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Domain validation exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DomainValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainValidationException(string message)
            : base(message)
        {
            this.Event = new DomainValidationApiEvent(message);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public DomainValidationApiEvent Event { get; }
    }
}
