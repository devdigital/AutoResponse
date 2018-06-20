// <copyright file="ResourceCreatedApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Dtos
{
    /// <summary>
    /// Resource created API model.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Dtos.ErrorApiModel" />
    public class ResourceCreatedApiModel : ErrorApiModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}