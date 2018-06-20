// <copyright file="ResourceValidationResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Results
{
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Resource validation result.
    /// </summary>
    /// <seealso cref="AutoResponse.AspNetCore.Results.AutoResponseResult" />
    public class ResourceValidationResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceValidationResult"/> class.
        /// </summary>
        /// <param name="errorDetails">The error details.</param>
        public ResourceValidationResult(ValidationErrorDetails errorDetails)
            : base(new EntityValidationApiEvent(errorDetails))
        {
        }
    }
}