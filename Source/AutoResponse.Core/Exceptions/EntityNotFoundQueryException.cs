// <copyright file="EntityNotFoundQueryException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;
    using System.Text;
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    /// <summary>
    /// Entity not found query exception.
    /// </summary>
    public class EntityNotFoundQueryException : AutoResponseException<EntityNotFoundQueryApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryException(string code, string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType: entityType, parameters: parameters), new EntityNotFoundQueryApiEvent(code, entityType, parameters))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType: entityType, parameters: parameters), new EntityNotFoundQueryApiEvent(entityType, parameters))
        {
        }

        private static string ToMessage(string entityType, IEnumerable<QueryParameter> parameters)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"The {entityType} entity with the provided parameters was not found:");

            foreach (var parameter in parameters)
            {
                stringBuilder.AppendLine(string.IsNullOrWhiteSpace(parameter.Value)
                    ? $"{parameter.Key}"
                    : $"{parameter.Key}: {parameter.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}