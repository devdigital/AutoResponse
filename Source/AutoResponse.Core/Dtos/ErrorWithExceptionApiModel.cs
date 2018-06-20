// <copyright file="ErrorWithExceptionApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Dtos
{
    /// <summary>
    /// Error with exception API model.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Dtos.ErrorApiModel" />
    public class ErrorWithExceptionApiModel : ErrorApiModel
    {
        /// <summary>
        /// Gets or sets the exception message.
        /// TODO: add exception type.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception string.
        /// </summary>
        /// <value>
        /// The exception string.
        /// </value>
        public string ExceptionString { get; set; }
    }
}