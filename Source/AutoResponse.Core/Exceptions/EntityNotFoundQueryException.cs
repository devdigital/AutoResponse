using System.Linq;

namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException : AutoResponseException<EntityNotFoundQueryApiEvent>
    {
        public EntityNotFoundQueryException(string code, string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType, parameters), new EntityNotFoundQueryApiEvent(code, entityType, parameters))
        {            
        }        

        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType, parameters), new EntityNotFoundQueryApiEvent(entityType, parameters))
        {            
        }

        private static string ToMessage(string entityType, IEnumerable<QueryParameter> parameters)
        {
            var keyValuePairs = parameters.Select(p => string.IsNullOrWhiteSpace(p.Value) 
                ? p.Key 
                : $"{p.Key} = {p.Value}");

            return $"The entity {entityType} was not found with parameters {string.Join(",", keyValuePairs)}";
        }
    }
}