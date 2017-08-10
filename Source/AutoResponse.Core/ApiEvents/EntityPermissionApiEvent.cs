namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class EntityPermissionApiEvent : IAutoResponseApiEvent
    {
        public EntityPermissionApiEvent(string code, string userId, string entityType, string entityId)
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

            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            this.Code = code;
            this.UserId = userId;
            this.EntityType = entityType;
            this.EntityId = entityId;
        }
       
        public EntityPermissionApiEvent(string userId, string entityType, string entityId) : this("AR403", userId, entityType, entityId)
        {         
        }

        public string Code { get; }

        public string UserId { get; }

        public string EntityType { get; }

        public string EntityId { get; }
    }
}