// <copyright file="ErrorDetailsApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Client.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Error details API model.
    /// </summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    public class ErrorDetailsApiModel<TError>
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<TError> Errors { get; set; }
    }
}