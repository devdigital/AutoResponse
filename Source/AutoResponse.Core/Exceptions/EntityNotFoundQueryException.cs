namespace AutoResponse.Core.Exceptions
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException : AutoResponseException
    {
        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base(new EntityNotFoundQueryApiEvent(entityType, parameters))
        {            
        }        
    }
}