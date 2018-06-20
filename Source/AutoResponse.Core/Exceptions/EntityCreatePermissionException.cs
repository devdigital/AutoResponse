// <copyright file="EntityCreatePermissionException.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    /// <summary>
    /// Entity create permission exception.
    /// </summary>
    public class EntityCreatePermissionException
        : AutoResponseException<EntityCreatePermissionApiEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityCreatePermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(code, userId, entityType, entityId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        public EntityCreatePermissionException(string userId, string entityType)
            : base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(userId, entityType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityCreatePermissionException(string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}