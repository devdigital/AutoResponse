// <copyright file="ResourceValidationHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Extensions;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Resource validation HTTP response.
    /// </summary>
    public class ResourceValidationHttpResponse : JsonHttpResponse<ResourceValidationApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceValidationHttpResponse"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="validationErrorDetails">The validation error details.</param>
        public ResourceValidationHttpResponse(string code, ValidationErrorDetails validationErrorDetails)
            : base(validationErrorDetails.ToDto(code), (HttpStatusCode)422)
        {
        }
    }
}