namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class EntityPermissionApiEvent : AutoResponseApiEvent
    {
        public EntityPermissionApiEvent(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
        }
        
        public EntityPermissionApiEvent(string userId, string entityType, string entityId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            this.UserId = userId;
            this.EntityType = entityType;
            this.EntityId = entityId;
        }

        public string Message { get; }

        public string UserId { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}