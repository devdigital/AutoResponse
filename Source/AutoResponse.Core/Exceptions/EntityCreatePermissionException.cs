namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityCreatePermissionException 
        : AutoResponseException<EntityCreatePermissionApiEvent>
    {
        public EntityCreatePermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user {userId} does not have permmission to create entity type {entityType} with entity id {entityId}", new EntityCreatePermissionApiEvent(code, userId, entityType, entityId))
        {
        }

        public EntityCreatePermissionException(string userId, string entityType) :
            base($"The user {userId} does not have permission to create entity type {entityType}", new EntityCreatePermissionApiEvent(userId, entityType))
        {            
        }

        public EntityCreatePermissionException(string userId, string entityType, string entityId)
            : base($"The user {userId} does not have permmission to create entity type {entityType} with entity id {entityId}", new EntityCreatePermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}