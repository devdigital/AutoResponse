// <copyright file="AutoResponseException{TEvent}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using System;

    /// <summary>
    /// AutoResponse exception.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public abstract class AutoResponseException<TEvent> : AutoResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResponseException{TEvent}"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="apiEvent">The API event.</param>
        protected AutoResponseException(string message, TEvent apiEvent)
            : base(message)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.Event = apiEvent;
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public TEvent Event { get; }

        /// <summary>
        /// Gets the event object.
        /// </summary>
        /// <value>
        /// The event object.
        /// </value>
        public override object EventObject => this.Event;
    }
}