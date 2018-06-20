// <copyright file="ResourceValidationApiModel.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// Resource validation API model.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.Dtos.ErrorApiModel" />
    public class ResourceValidationApiModel : ErrorApiModel
    {
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<ResourceValidationErrorApiModel> Errors { get; set; }
    }
}