namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException<TEntity> : EntityNotFoundQueryException
    {
        public EntityNotFoundQueryException(IEnumerable<QueryParameter> parameters)
            : base(typeof(TEntity).Name, parameters)
        {
        }
    }
}