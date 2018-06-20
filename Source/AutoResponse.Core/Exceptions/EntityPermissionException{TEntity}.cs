// <copyright file="EntityPermissionException{TEntity}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    /// <summary>
    /// Entity permission exception.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="AutoResponse.Core.Exceptions.EntityPermissionException" />
    public class EntityPermissionException<TEntity> : EntityPermissionException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPermissionException{TEntity}"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityPermissionException(string code, string userId, string entityId)
            : base(code, userId, typeof(TEntity).Name, entityId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPermissionException{TEntity}"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityPermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}