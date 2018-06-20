// <copyright file="ResourceNotFoundQueryApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// Resource not found query API model.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Dtos.ErrorApiModel" />
    public class ResourceNotFoundQueryApiModel : ErrorApiModel
    {
        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>
        /// The resource.
        /// </value>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>
        /// The query parameters.
        /// </value>
        public IEnumerable<QueryParameterDto> QueryParameters { get; set; }
    }
}