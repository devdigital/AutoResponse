// <copyright file="EntityValidationApiEvent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.ApiEvents
{
    using System;

    using AutoResponse.Core.Models;

    /// <summary>
    /// Entity validation API event.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.ApiEvents.IAutoResponseApiEvent" />
    public class EntityValidationApiEvent : IAutoResponseApiEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationApiEvent"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errorDetails">The error details.</param>
        public EntityValidationApiEvent(string code, ValidationErrorDetails errorDetails)
        {
            this.Code = code;
            this.ErrorDetails = errorDetails ?? throw new ArgumentNullException(nameof(errorDetails));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationApiEvent"/> class.
        /// </summary>
        /// <param name="errorDetails">The error details.</param>
        public EntityValidationApiEvent(ValidationErrorDetails errorDetails)
            : this("AR422", errorDetails)
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
        /// Gets the error details.
        /// </summary>
        /// <value>
        /// The error details.
        /// </value>
        public ValidationErrorDetails ErrorDetails { get; }
    }
}