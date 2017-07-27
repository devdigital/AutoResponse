namespace AutoResponse.Core.ApiEvents
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryApiEvent
    {
        public EntityNotFoundQueryApiEvent(string entityType, IEnumerable<QueryParameter> parameters)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            this.EntityType = entityType;
            this.Parameters = parameters;
        }

        public string EntityType { get; }

        public IEnumerable<QueryParameter> Parameters { get; }
    }
}