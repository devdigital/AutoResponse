// <copyright file="EntityValidationException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Entity validation exception.
    /// </summary>
    public class EntityValidationException : AutoResponseException<EntityValidationApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errorDetails">The error details.</param>
        public EntityValidationException(string code, ValidationErrorDetails errorDetails)
            : base(errorDetails.Message, new EntityValidationApiEvent(code, errorDetails))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidationException"/> class.
        /// </summary>
        /// <param name="errorDetails">The error details.</param>
        public EntityValidationException(ValidationErrorDetails errorDetails)
            : base(errorDetails.Message, new EntityValidationApiEvent(errorDetails))
        {
        }
    }
}