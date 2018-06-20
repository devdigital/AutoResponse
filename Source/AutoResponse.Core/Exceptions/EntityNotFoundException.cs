// <copyright file="EntityNotFoundException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Entity not found exception.
    /// </summary>
    public class EntityNotFoundException : AutoResponseException<EntityNotFoundApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityNotFoundException(string code, string entityType, string entityId)
            : base($"The {entityType} entity with identifier {entityId} was not found.", new EntityNotFoundApiEvent(code, entityType, entityId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityNotFoundException(string entityType, string entityId)
            : base($"The {entityType} entity with identifier {entityId} was not found.", new EntityNotFoundApiEvent(entityType, entityId))
        {
        }
    }
}
