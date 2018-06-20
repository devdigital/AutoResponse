// <copyright file="EntityNotFoundException{TEntity}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    /// <summary>
    /// Entity not found exception.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="AutoResponse.Core.Exceptions.EntityNotFoundException" />
    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="entityId">The entity identifier.</param>
        public EntityNotFoundException(string code, string entityId)
            : base(code, typeof(TEntity).Name, entityId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        public EntityNotFoundException(string entityId)
            : base(typeof(TEntity).Name, entityId)
        {
        }
    }
}