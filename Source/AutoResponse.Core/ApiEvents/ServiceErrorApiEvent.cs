// <copyright file="ServiceErrorApiEvent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.ApiEvents
{
    using System;

    /// <summary>
    /// Service error API event.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.ApiEvents.IAutoResponseApiEvent" />
    public class ServiceErrorApiEvent : IAutoResponseApiEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorApiEvent"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ServiceErrorApiEvent(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(nameof(message)))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorApiEvent"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="exception">The exception.</param>
        public ServiceErrorApiEvent(string code, Exception exception)
        {
            this.Code = code;
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorApiEvent"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ServiceErrorApiEvent(string message)
            : this("AR500", message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceErrorApiEvent"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ServiceErrorApiEvent(Exception exception)
            : this("AR500", exception)
        {
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; }
    }
}