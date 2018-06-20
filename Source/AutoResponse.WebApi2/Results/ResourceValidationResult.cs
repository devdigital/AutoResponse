// <copyright file="ResourceValidationResult.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.Results
{
    using System.Net.Http;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Resource validation result.
    /// </summary>
    /// <seealso cref="AutoResponse.WebApi2.Results.AutoResponseResult" />
    public class ResourceValidationResult : AutoResponseResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceValidationResult"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="errorDetails">The error details.</param>
        public ResourceValidationResult(HttpRequestMessage request, ValidationErrorDetails errorDetails)
            : base(request, new EntityValidationApiEvent(errorDetails))
        {
        }
    }
}