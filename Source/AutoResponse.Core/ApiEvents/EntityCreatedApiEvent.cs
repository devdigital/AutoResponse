namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class EntityCreatedApiEvent : IAutoResponseApiEvent
    {
        public EntityCreatedApiEvent(string code, string userId, string entityType, string entityId)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            this.Code = code;
            this.UserId = userId;
            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        public EntityCreatedApiEvent(string userId, string entityType) : this("AR201", userId, entityType, null)
        {            
        }

        public EntityCreatedApiEvent(string userId, string entityType, string entityId) : this("AR201", userId, entityType, entityId)
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }
        }

        public string Code { get; }
        
        public string UserId { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}