// <copyright file="EntityNotFoundQueryApiEvent.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.ApiEvents
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    /// <summary>
    /// Entity not found query API event.
    /// </summary>
    /// <seealso cref="AutoResponse.Core.ApiEvents.IAutoResponseApiEvent" />
    public class EntityNotFoundQueryApiEvent : IAutoResponseApiEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryApiEvent"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryApiEvent(string code, string entityType, IEnumerable<QueryParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            this.Code = code;
            this.EntityType = entityType;
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryApiEvent"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryApiEvent(string entityType, IEnumerable<QueryParameter> parameters)
            : this("AR404Q", entityType, parameters)
        {
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>
        /// The type of the entity.
        /// </value>
        public string EntityType { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IEnumerable<QueryParameter> Parameters { get; }
    }
}