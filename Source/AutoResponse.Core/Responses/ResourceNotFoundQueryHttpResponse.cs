// <copyright file="ResourceNotFoundQueryHttpResponse.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using AutoResponse.Core.Dtos;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Resource not found query HTTP response.
    /// </summary>
    public class ResourceNotFoundQueryHttpResponse : JsonHttpResponse<ResourceNotFoundQueryApiModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundQueryHttpResponse"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        public ResourceNotFoundQueryHttpResponse(string message, string code, string resource, IEnumerable<QueryParameter> parameters)
            : base(new ResourceNotFoundQueryApiModel { Message = message, Code = code, Resource = resource, QueryParameters = parameters.Select(p => new QueryParameterDto { Key = p.Key, Value = p.Value }) }, HttpStatusCode.NotFound)
        {
        }
    }
}