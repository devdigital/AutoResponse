namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException : AutoResponseException<EntityNotFoundQueryApiEvent>
    {
        public EntityNotFoundQueryException(string code, string entityType, IEnumerable<QueryParameter> parameters)
            : base($"The {entityType} entity with the provided parameters was not found.", new EntityNotFoundQueryApiEvent(code, entityType, parameters))
        {            
        }

        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base($"The {entityType} entity with the provided parameters was not found.", new EntityNotFoundQueryApiEvent(entityType, parameters))
        {            
        }        
    }
}