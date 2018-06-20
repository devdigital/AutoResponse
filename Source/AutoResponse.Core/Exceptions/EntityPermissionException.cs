// <copyright file="EntityPermissionException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Entity permission exception.
    /// </summary>
    public class EntityPermissionException : AutoResponseException<EntityPermissionApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPermissionException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityPermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to access the {entityType} entity with identifier '{entityId}'", new EntityPermissionApiEvent(code, userId, entityType, entityId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPermissionException"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityPermissionException(string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to access the {entityType} entity with identifier '{entityId}'", new EntityPermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}