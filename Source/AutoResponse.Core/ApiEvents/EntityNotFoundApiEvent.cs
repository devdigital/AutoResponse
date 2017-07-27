namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class EntityNotFoundApiEvent
    {
        public EntityNotFoundApiEvent(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
        }

        public EntityNotFoundApiEvent(string entityType, string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        public string Message { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}