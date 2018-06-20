// <copyright file="EntityNotFoundQueryException{TEntity}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    /// <summary>
    /// Entity not found query exception.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="AutoResponse.Core.Exceptions.EntityNotFoundQueryException" />
    public class EntityNotFoundQueryException<TEntity> : EntityNotFoundQueryException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryException{TEntity}"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryException(string code, IEnumerable<QueryParameter> parameters)
            : base(code, typeof(TEntity).Name, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundQueryException{TEntity}"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public EntityNotFoundQueryException(IEnumerable<QueryParameter> parameters)
            : base(typeof(TEntity).Name, parameters)
        {
        }
    }
}