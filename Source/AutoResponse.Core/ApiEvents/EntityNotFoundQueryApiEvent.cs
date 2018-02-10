namespace AutoResponse.Core.ApiEvents
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryApiEvent : IAutoResponseApiEvent
    {
        public EntityNotFoundQueryApiEvent(string code, string entityType, IEnumerable<QueryParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            this.Code = code;
            this.EntityType = entityType;
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public EntityNotFoundQueryApiEvent(string entityType, IEnumerable<QueryParameter> parameters) : this("AR404Q", entityType, parameters)
        {            
        }

        public string Code { get; }

        public string EntityType { get; }

        public IEnumerable<QueryParameter> Parameters { get; }
    }
}