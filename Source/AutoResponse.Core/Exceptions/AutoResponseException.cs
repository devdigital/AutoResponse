// <copyright file="AutoResponseException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using System;

    /// <summary>
    /// AutoResponse exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class AutoResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected AutoResponseException(string message)
            : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
        }

        /// <summary>
        /// Gets the event object.
        /// </summary>
        /// <value>
        /// The event object.
        /// </value>
        public abstract object EventObject { get; }
    }
}