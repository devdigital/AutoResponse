using System.Text;

namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityNotFoundQueryException : AutoResponseException<EntityNotFoundQueryApiEvent>
    {
        public EntityNotFoundQueryException(string code, string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType: entityType, parameters: parameters), new EntityNotFoundQueryApiEvent(code, entityType, parameters))
        {            
        }

        public EntityNotFoundQueryException(string entityType, IEnumerable<QueryParameter> parameters)
            : base(ToMessage(entityType: entityType, parameters: parameters), new EntityNotFoundQueryApiEvent(entityType, parameters))
        {            
        }

        private static string ToMessage(string entityType, IEnumerable<QueryParameter> parameters)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"The {entityType} entity with the provided parameters was not found:");

            foreach (var parameter in parameters)
            {
                stringBuilder.AppendLine(string.IsNullOrWhiteSpace(parameter.Value)
                    ? $"{parameter.Key}"
                    : $"{parameter.Key}: {parameter.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}