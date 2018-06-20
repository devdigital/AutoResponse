// <copyright file="ResourcePermissionApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Dtos
{
    /// <summary>
    /// Resource permission API model.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Dtos.ErrorApiModel" />
    public class ResourcePermissionApiModel : ErrorApiModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>
        /// The resource.
        /// </value>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public string ResourceId { get; set; }
    }
}