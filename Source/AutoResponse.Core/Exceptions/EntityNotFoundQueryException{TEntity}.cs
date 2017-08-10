namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException<TEntity> : EntityNotFoundQueryException
    {
        public EntityNotFoundQueryException(string code, IEnumerable<QueryParameter> parameters)
            : base(code, typeof(TEntity).Name, parameters)
        {            
        }

        public EntityNotFoundQueryException(IEnumerable<QueryParameter> parameters)
            : base(typeof(TEntity).Name, parameters)
        {
        }
    }
}