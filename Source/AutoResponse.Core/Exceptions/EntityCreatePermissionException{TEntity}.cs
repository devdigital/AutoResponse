// <copyright file="EntityCreatePermissionException{TEntity}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    /// <summary>
    /// Entity create permission exception.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="AutoResponse.Core.Exceptions.EntityCreatePermissionException" />
    public class EntityCreatePermissionException<TEntity> : EntityCreatePermissionException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException{TEntity}"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityCreatePermissionException(string code, string userId, string entityType, string entityId)
            : base(code, userId, entityType, entityId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException{TEntity}"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public EntityCreatePermissionException(string userId)
            : base(userId, typeof(TEntity).Name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreatePermissionException{TEntity}"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityCreatePermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}