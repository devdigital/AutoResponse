namespace AutoResponse.Core.ApiEvents
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryApiEvent : IAutoResponseApiEvent
    {
        public EntityNotFoundQueryApiEvent(string code, string entityType, IEnumerable<QueryParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            this.Code = code;
            this.EntityType = entityType;
            this.Parameters = parameters;
        }

        public EntityNotFoundQueryApiEvent(string entityType, IEnumerable<QueryParameter> parameters) : this("AR404Q", entityType, parameters)
        {            
        }

        public string Code { get; }

        public string EntityType { get; }

        public IEnumerable<QueryParameter> Parameters { get; }
    }
}