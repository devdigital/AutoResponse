namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException : AutoResponseException
    {
        public EntityNotFoundQueryException(string code, string entityType, IEnumerable<QueryParameter> parameters)
            : base(new EntityNotFoundQueryApiEvent(code, entityType, parameters))
        {            
        }

        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base(new EntityNotFoundQueryApiEvent(entityType, parameters))
        {            
        }        
    }
}