namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class EntityNotFoundApiEvent : IAutoResponseApiEvent
    {
        public EntityNotFoundApiEvent(string code, string entityType, string entityId)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            this.Code = code;
            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        public EntityNotFoundApiEvent(string entityType, string entityId) : this("AR404", entityType, entityId)
        {
        }

        public string Code { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}