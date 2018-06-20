// <copyright file="ValidationErrorCode.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Enums
{
    /// <summary>
    /// Validation error code.
    /// </summary>
    public enum ValidationErrorCode
    {
        /// <summary>
        /// No validation code.
        /// </summary>
        None = 0,

        /// <summary>
        /// Missing resource.
        /// </summary>
        Missing,

        /// <summary>
        /// Missing field.
        /// </summary>
        MissingField,

        /// <summary>
        /// Invalid field.
        /// </summary>
        Invalid,

        /// <summary>
        /// Resource already exists.
        /// </summary>
        AlreadyExists,
    }
}