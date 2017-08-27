namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityPermissionException : AutoResponseException
    {
        public EntityPermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user {userId} does not have permission to access entity {entityType} with entity id {entityId}", new EntityPermissionApiEvent(code, userId, entityType, entityId))
        {            
        }

        public EntityPermissionException(string userId, string entityType, string entityId)
            : base($"The user {userId} does not have permission to access entity {entityType} with entity id {entityId}", new EntityPermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}