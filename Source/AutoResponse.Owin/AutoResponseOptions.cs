// <copyright file="AutoResponseOptions.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Owin
{
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;

    /// <summary>
    /// AutoResponse options.
    /// </summary>
    public class AutoResponseOptions
    {
        /// <summary>
        /// Gets or sets the event HTTP response mapper.
        /// </summary>
        /// <value>
        /// The event HTTP response mapper.
        /// </value>
        public IApiEventHttpResponseMapper EventHttpResponseMapper { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public IAutoResponseLogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain result property.
        /// </summary>
        /// <value>
        /// The name of the domain result property.
        /// </value>
        public string DomainResultPropertyName { get; set; }
    }
}