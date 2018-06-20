// <copyright file="DomainValidationApiEvent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Exceptions
{
    /// <summary>
    /// Domain validation API event.
    /// </summary>
    public class DomainValidationApiEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidationApiEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DomainValidationApiEvent(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }
    }
}