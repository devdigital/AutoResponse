// <copyright file="ValidationError.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Models
{
    using System;

    using AutoResponse.Core.Enums;

    /// <summary>
    /// Validation error.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="field">The field.</param>
        /// <param name="code">The code.</param>
        /// <param name="errorMessage">The error message.</param>
        public ValidationError(string resource, string field, ValidationErrorCode code, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (code == ValidationErrorCode.None)
            {
                throw new ArgumentNullException(nameof(code));
            }

            this.Resource = resource;
            this.Field = field;
            this.Code = code;
            this.Message = errorMessage;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>
        /// The resource.
        /// </value>
        public string Resource { get; }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>
        /// The field.
        /// </value>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public ValidationErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}